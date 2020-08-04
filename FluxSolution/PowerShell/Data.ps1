# Author and contact: ([adsisearcher]"(&(objectClass=User)(mail=Robert.Hugo@pima.gov))").FindOne().GetDirectoryEntry().Properties | ft

function Get-AppDomainName {[System.AppDomain]::CurrentDomain.FriendlyName} # Returns the file name of the process executable used to start it.

function Get-ComputerDomainName {[System.Net.NetworkInformation.IPGlobalProperties]::GetIPGlobalProperties().DomainName}
function Get-ComputerHostName {[System.Net.NetworkInformation.IPGlobalProperties]::GetIPGlobalProperties().HostName}
function Get-ComputerPrimaryDnsName {[System.Net.Dns]::GetHostEntry('LocalHost').HostName}

function Get-FrameworkDescription {[System.Runtime.InteropServices.RuntimeInformation]::FrameworkDescription}

function Get-MachineName {[System.Environment]::MachineName}

function Get-OperatingSystemDescription {[System.Runtime.InteropServices.RuntimeInformation]::OSDescription}

function Get-UserDomainName {[System.Environment]::UserDomainName}
function Get-UserName {[System.Environment]::UserName}

function Get-CsApplicationName {"$(Get-AppDomainName)"}
function Get-CsWorkstationID {"$(@((Get-ComputerHostName),(Get-ComputerPrimaryDnsName),(Get-MachineName)) | sort Length -Descending | select -First 1)"}

function Get-SqlSysDatabases {@('DataStore','master','model','msdb','ReportServer','ReportServerTempDB','SSISDB','SSISLogging','tempdb')}
function Get-SqlSysRoles {@('db_accessadmin','db_backupoperator','db_datareader','db_datawriter','db_ddladmin','db_denydatareader','db_denydatawriter','db_owner','db_securityadmin','public')}
function Get-SqlSysSchemas {@('db_accessadmin','db_backupoperator','db_datareader','db_datawriter','db_ddladmin','db_denydatareader','db_denydatawriter','db_owner','db_securityadmin','dbo','guest','INFORMATION_SCHEMA','sys')}

# Merge the properties from two PSObject into a single PSObject
function Merge-Objects {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)]$Object1,
        [Parameter(Mandatory=$true)]$Object2,
        [Parameter(Mandatory=$true)][ValidateSet('Array','Keep','Replace')][string]$DuplicateBehavior = 'Keep'
    )
    $pso = @{}
    foreach ($p in $Object1 | Get-Member | ? {$_.MemberType -eq 'NoteProperty' }) {
        $pso.Add($p.Name, $Object1."$($p.Name)")
    }
    foreach ($p in $Object2 | Get-Member | ? {$_.MemberType -eq 'NoteProperty' }) {
        if(-not $pso.Contains($p.Name)) {
            $pso.Add($p.Name, $Object2."$($p.Name)")
        }
        else { # A property with the same name exists.
            switch($DuplicateBehavior) {
                'Array' {$pso[$p.Name] = @($pso[$p.Name], $Object2."$($p.Name)")}
                'Replace' {$pso[$p.Name] = $Object2."$($p.Name)"}
                # Keep the first value that was found.
            }
        }
    }
    [PSCustomObject]$pso
}

function Convert-KeyValuePairsToString([hashtable]$KeyValuePair){@(foreach($kvp in $KeyValuePair){$kvp.GetEnumerator() | % {(&{if($_.Key -and $_.Value){"$($_.Key)=$($_.Value)"}})}}) -join ';'}

# Converts the data from an IDataReader into a DataTable.
function Convert-IDataReaderToDataTable {
    [CmdletBinding()] param (
        [parameter(Mandatory=$true,ValueFromPipeline=$true)][System.Data.IDataReader]$IDataReader
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        Write-Verbose "$($MyInvocation.MyCommand.Name)"
        $dt = New-Object System.Data.DataTable
        $dt.Load($IDataReader)
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"
        (,$dt)
    }
	catch { throw; }
    finally { if($IDataReader) { $IDataReader.Dispose() } }
}

# Converts the data from an IDataReader to a stream of PSObject.
function ConvertFrom-IDataReader {
    [CmdletBinding()] param (
        [parameter(Mandatory=$true,ValueFromPipeline=$true)][System.Data.IDataReader]$IDataReader
    )
    try {
        Write-Verbose "$($MyInvocation.MyCommand.Name) (to a stream of PSObject)"
        do {
            while($IDataReader.Read()) {
                $ht = [ordered]@{}
                for($i = 0; $i -lt $IDataReader.FieldCount; $i++) { $ht[$IDataReader.GetName($i)] = if($IDataReader.IsDBNull($i)){$null}else{$IDataReader.GetValue($i)} }
                [PSCustomObject]$ht
            }
        }
        while($IDataReader.NextResult())
    }
	catch { throw; }
    finally { if($IDataReader) { $IDataReader.Dispose() } }
}

# Converts a stream of PSObject into a DataTable.
function ConvertTo-DataTable {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true,ValueFromPipeline=$true)][psobject[]]$InputObject,
        [string]$DefaultType = 'System.String'
    )
    begin {
        Write-Verbose "$($MyInvocation.MyCommand.Name)"
        $dt = New-Object System.Data.DataTable
        $types = @([System.TypeCode]::GetValues([System.TypeCode]) + @('Guid') | ? {$_ -inotin 'Empty','DBNull'} | % {"System.$_"; if($_ -iin 'Byte'){"System.$_[]"}} | sort)
    }
    process {
        foreach ($object in $InputObject) {
            $dr = $dt.NewRow()
            foreach($pi in $object.PSObject.Properties) {
                if(-not $dt.Columns.Contains($pi.Name)) { # Add column if it doesn't exists.
                    $dc = New-Object System.Data.DataColumn($pi.Name)
                    if ($pi.Value -ne $null -and $pi.Value -isnot [System.DBNull]) {
                        if($types -icontains $pi.TypeNameOfValue) {$dc.DataType = [System.Type]::GetType($pi.TypeNameOfValue)}
                        else {$dc.DataType = [System.Object]}
                    }
                    $dt.Columns.Add($dc)
                }

                if($pi.Value -eq $null) { $dr[$pi.Name] = [System.DBNull]::Value }
                elseif ($pi.GetType().IsArray) {
                    if($pi.Value -is [byte[]]) {$dr[$pi.Name] = $pi.Value} # Keep byte arrays.
                    elseif($pi.Value -is [char[]]) {$dr[$pi.Name] = $pi.Value} # Keep byte arrays.
                    $dr[$pi.Name] = ($pi.Value | ConvertTo-Xml -As String -NoTypeInformation -Depth 1)
                }
                else { $dr[$pi.Name] = $pi.Value }
            }
            $dt.Rows.Add($dr)
        } 
    }
    end {
        @(,($dt))
    }
}

# Returns data from INFORMATION_SCHEMA views.
function Export-SqlInformationSchemaView {
    [CmdletBinding()] Param (
        [Parameter(Mandatory=$true)][string[]]$SqlInstance,
        [Parameter(Mandatory=$true)][string[]]$DatabaseName,
        [Parameter(Mandatory=$true)][ValidateSet('CHECK_CONSTRAINTS','COLUMN_DOMAIN_USAGE','COLUMN_PRIVILEGES','COLUMNS','CONSTRAINT_COLUMN_USAGE','CONSTRAINT_TABLE_USAGE','DOMAIN_CONSTRAINTS','DOMAINS','KEY_COLUMN_USAGE','PARAMETERS','REFERENTIAL_CONSTRAINTS','ROUTINE_COLUMNS','ROUTINES','SCHEMATA','SEQUENCES','TABLE_CONSTRAINTS','TABLE_PRIVILEGES','TABLES','VIEW_COLUMN_USAGE','VIEW_TABLE_USAGE','VIEWS')][string]$View,
        [switch]$IncludeMetaData
    )
    begin {
        $commandText = "
SELECT
$(if($IncludeMetaData){"
    CONVERT(sysname, SERVERPROPERTY('servername')) AS [SqlInstance],
    DB_NAME() AS [CatalogName],
    '$View' AS [CatalogView],
"})
    *
FROM INFORMATION_SCHEMA.$View;
"
    }
    process {
        foreach($instance in $SqlInstance) {
            foreach($database in $DatabaseName) {
                Invoke-SqlReader -ConnectionString (Format-SqlConnectionString -SqlInstance $instance -DatabaseName $database) -CommandText $commandText | ConvertFrom-IDataReader
            } 
        } 
    }
}
# Returns data from System Catalog (sys) views.
function Export-SqlSystemCatalogView {
    [CmdletBinding()] Param (
        [Parameter(Mandatory=$true)][string[]]$SqlInstance,
        [string[]]$DatabaseName = 'master',
        [Parameter(Mandatory=$true)][ValidateSet('all_columns','all_objects','all_parameters','all_sql_modules','all_views','allocation_units','assemblies','assembly_files','assembly_modules','assembly_references','assembly_types','asymmetric_keys','availability_databases_cluster','availability_group_listener_ip_addresses','availability_group_listeners','availability_groups','availability_groups_cluster','availability_read_only_routing_lists','availability_replicas','backup_devices','certificates','change_tracking_databases','change_tracking_tables','check_constraints','column_encryption_key_values','column_encryption_keys','column_master_keys','column_store_dictionaries','column_store_row_groups','column_store_segments','column_type_usages','column_xml_schema_collection_usages','columns','computed_columns','configurations','conversation_endpoints','conversation_groups','conversation_priorities','credentials','crypt_properties','cryptographic_providers','data_spaces','database_audit_specification_details','database_audit_specifications','database_credentials','database_files','database_filestream_options','database_mirroring','database_mirroring_endpoints','database_mirroring_witnesses','database_permissions','database_principals','database_query_store_options','database_recovery_status','database_role_members','database_scoped_configurations','database_scoped_credentials','databases','default_constraints','destination_data_spaces','dm_audit_actions','dm_audit_class_type_map','dm_broker_activated_tasks','dm_broker_connections','dm_broker_forwarded_messages','dm_broker_queue_monitors','dm_cdc_errors','dm_cdc_log_scan_sessions','dm_clr_appdomains','dm_clr_loaded_assemblies','dm_clr_properties','dm_clr_tasks','dm_column_store_object_pool','dm_cryptographic_provider_properties','dm_database_encryption_keys','dm_db_column_store_row_group_operational_stats','dm_db_column_store_row_group_physical_stats','dm_db_file_space_usage','dm_db_fts_index_physical_stats','dm_db_index_usage_stats','dm_db_log_space_usage','dm_db_mirroring_auto_page_repair','dm_db_mirroring_connections','dm_db_mirroring_past_actions','dm_db_missing_index_details','dm_db_missing_index_group_stats','dm_db_missing_index_groups','dm_db_partition_stats','dm_db_persisted_sku_features','dm_db_rda_migration_status','dm_db_rda_schema_update_status','dm_db_script_level','dm_db_session_space_usage','dm_db_task_space_usage','dm_db_uncontained_entities','dm_db_xtp_checkpoint_files','dm_db_xtp_checkpoint_stats','dm_db_xtp_gc_cycle_stats','dm_db_xtp_hash_index_stats','dm_db_xtp_index_stats','dm_db_xtp_memory_consumers','dm_db_xtp_nonclustered_index_stats','dm_db_xtp_object_stats','dm_db_xtp_table_memory_stats','dm_db_xtp_transactions','dm_exec_background_job_queue','dm_exec_background_job_queue_stats','dm_exec_cached_plans','dm_exec_compute_node_errors','dm_exec_compute_node_status','dm_exec_compute_nodes','dm_exec_connections','dm_exec_distributed_request_steps','dm_exec_distributed_requests','dm_exec_distributed_sql_requests','dm_exec_dms_services','dm_exec_dms_workers','dm_exec_external_operations','dm_exec_external_work','dm_exec_function_stats','dm_exec_procedure_stats','dm_exec_query_memory_grants','dm_exec_query_optimizer_info','dm_exec_query_optimizer_memory_gateways','dm_exec_query_parallel_workers','dm_exec_query_profiles','dm_exec_query_resource_semaphores','dm_exec_query_stats','dm_exec_query_transformation_stats','dm_exec_requests','dm_exec_session_wait_stats','dm_exec_sessions','dm_exec_trigger_stats','dm_exec_valid_use_hints','dm_external_script_execution_stats','dm_external_script_requests','dm_filestream_file_io_handles','dm_filestream_file_io_requests','dm_filestream_non_transacted_handles','dm_fts_active_catalogs','dm_fts_fdhosts','dm_fts_index_population','dm_fts_memory_buffers','dm_fts_memory_pools','dm_fts_outstanding_batches','dm_fts_population_ranges','dm_fts_semantic_similarity_population','dm_hadr_auto_page_repair','dm_hadr_automatic_seeding','dm_hadr_availability_group_states','dm_hadr_availability_replica_cluster_nodes','dm_hadr_availability_replica_cluster_states','dm_hadr_availability_replica_states','dm_hadr_cluster','dm_hadr_cluster_members','dm_hadr_cluster_networks','dm_hadr_database_replica_cluster_states','dm_hadr_database_replica_states','dm_hadr_instance_node_map','dm_hadr_name_id_map','dm_hadr_physical_seeding_stats','dm_io_backup_tapes','dm_io_cluster_shared_drives','dm_io_cluster_valid_path_names','dm_io_pending_io_requests','dm_logpool_hashentries','dm_logpool_stats','dm_os_buffer_descriptors','dm_os_buffer_pool_extension_configuration','dm_os_child_instances','dm_os_cluster_nodes','dm_os_cluster_properties','dm_os_dispatcher_pools','dm_os_dispatchers','dm_os_hosts','dm_os_latch_stats','dm_os_loaded_modules','dm_os_memory_allocations','dm_os_memory_broker_clerks','dm_os_memory_brokers','dm_os_memory_cache_clock_hands','dm_os_memory_cache_counters','dm_os_memory_cache_entries','dm_os_memory_cache_hash_tables','dm_os_memory_clerks','dm_os_memory_node_access_stats','dm_os_memory_nodes','dm_os_memory_objects','dm_os_memory_pools','dm_os_nodes','dm_os_performance_counters','dm_os_process_memory','dm_os_ring_buffers','dm_os_schedulers','dm_os_server_diagnostics_log_configurations','dm_os_spinlock_stats','dm_os_stacks','dm_os_sublatches','dm_os_sys_info','dm_os_sys_memory','dm_os_tasks','dm_os_threads','dm_os_virtual_address_dump','dm_os_wait_stats','dm_os_waiting_tasks','dm_os_windows_info','dm_os_worker_local_storage','dm_os_workers','dm_qn_subscriptions','dm_repl_articles','dm_repl_schemas','dm_repl_tranhash','dm_repl_traninfo','dm_resource_governor_configuration','dm_resource_governor_external_resource_pool_affinity','dm_resource_governor_external_resource_pools','dm_resource_governor_resource_pool_affinity','dm_resource_governor_resource_pool_volumes','dm_resource_governor_resource_pools','dm_resource_governor_workload_groups','dm_server_audit_status','dm_server_memory_dumps','dm_server_registry','dm_server_services','dm_tcp_listener_states','dm_tran_active_snapshot_database_transactions','dm_tran_active_transactions','dm_tran_commit_table','dm_tran_current_snapshot','dm_tran_current_transaction','dm_tran_database_transactions','dm_tran_global_recovery_transactions','dm_tran_global_transactions','dm_tran_global_transactions_enlistments','dm_tran_global_transactions_log','dm_tran_locks','dm_tran_session_transactions','dm_tran_top_version_generators','dm_tran_transactions_snapshot','dm_tran_version_store','dm_tran_version_store_space_usage','dm_xe_map_values','dm_xe_object_columns','dm_xe_objects','dm_xe_packages','dm_xe_session_event_actions','dm_xe_session_events','dm_xe_session_object_columns','dm_xe_session_targets','dm_xe_sessions','dm_xtp_gc_queue_stats','dm_xtp_gc_stats','dm_xtp_system_memory_consumers','dm_xtp_threads','dm_xtp_transaction_recent_rows','dm_xtp_transaction_stats','endpoint_webmethods','endpoints','event_notification_event_types','event_notifications','events','extended_procedures','extended_properties','external_data_sources','external_file_formats','external_tables','filegroups','filetable_system_defined_objects','filetables','foreign_key_columns','foreign_keys','fulltext_catalogs','fulltext_document_types','fulltext_index_catalog_usages','fulltext_index_columns','fulltext_index_fragments','fulltext_indexes','fulltext_languages','fulltext_semantic_language_statistics_database','fulltext_semantic_languages','fulltext_stoplists','fulltext_stopwords','fulltext_system_stopwords','function_order_columns','hash_indexes','http_endpoints','identity_columns','index_columns','indexes','internal_partitions','internal_tables','key_constraints','key_encryptions','linked_logins','login_token','masked_columns','master_files','master_key_passwords','memory_optimized_tables_internal_attributes','message_type_xml_schema_collection_usages','messages','module_assembly_usages','numbered_procedure_parameters','numbered_procedures','objects','openkeys','parameter_type_usages','parameter_xml_schema_collection_usages','parameters','partition_functions','partition_parameters','partition_range_values','partition_schemes','partitions','periods','plan_guides','procedures','query_context_settings','query_store_plan','query_store_query','query_store_query_text','query_store_runtime_stats','query_store_runtime_stats_interval','registered_search_properties','registered_search_property_lists','remote_data_archive_databases','remote_data_archive_tables','remote_logins','remote_service_bindings','resource_governor_configuration','resource_governor_external_resource_pool_affinity','resource_governor_external_resource_pools','resource_governor_resource_pool_affinity','resource_governor_resource_pools','resource_governor_workload_groups','routes','schemas','securable_classes','security_policies','security_predicates','selective_xml_index_namespaces','selective_xml_index_paths','sequences','server_assembly_modules','server_audit_specification_details','server_audit_specifications','server_audits','server_event_notifications','server_event_session_actions','server_event_session_events','server_event_session_fields','server_event_session_targets','server_event_sessions','server_events','server_file_audits','server_permissions','server_principal_credentials','server_principals','server_role_members','server_sql_modules','server_trigger_events','server_triggers','servers','service_broker_endpoints','service_contract_message_usages','service_contract_usages','service_contracts','service_message_types','service_queue_usages','service_queues','services','soap_endpoints','spatial_index_tessellations','spatial_indexes','spatial_reference_systems','sql_dependencies','sql_expression_dependencies','sql_logins','sql_modules','stats','stats_columns','symmetric_keys','synonyms','sysaltfiles','syscacheobjects','syscharsets','syscolumns','syscomments','sysconfigures','sysconstraints','syscurconfigs','syscursorcolumns','syscursorrefs','syscursors','syscursortables','sysdatabases','sysdepends','sysdevices','sysfilegroups','sysfiles','sysforeignkeys','sysfulltextcatalogs','sysindexes','sysindexkeys','syslanguages','syslockinfo','syslogins','sysmembers','sysmessages','sysobjects','sysoledbusers','sysopentapes','sysperfinfo','syspermissions','sysprocesses','sysprotects','sysreferences','sysremotelogins','sysservers','system_columns','system_components_surface_area_configuration','system_internals_allocation_units','system_internals_partition_columns','system_internals_partitions','system_objects','system_parameters','system_sql_modules','system_views','systypes','sysusers','table_types','tables','tcp_endpoints','time_zone_info','trace_categories','trace_columns','trace_event_bindings','trace_events','trace_subclass_values','traces','transmission_queue','trigger_event_types','trigger_events','triggers','type_assembly_usages','types','user_token','via_endpoints','views','xml_indexes','xml_schema_attributes','xml_schema_collections','xml_schema_component_placements','xml_schema_components','xml_schema_elements','xml_schema_facets','xml_schema_model_groups','xml_schema_namespaces','xml_schema_types','xml_schema_wildcard_namespaces','xml_schema_wildcards')][string]$View,
        [switch]$IncludeMetaData
    )
    begin {
        $commandText = "
SELECT
$(if($IncludeMetaData){"
    CONVERT(sysname, SERVERPROPERTY('servername')) AS [ServerName],
    DB_NAME() AS [CatalogName],
    '$View' AS [CatalogView],
"})
    *
FROM sys.$View;
"
    }
    process {
        foreach($instance in $SqlInstance) {
            foreach($database in $DatabaseName) {
                Invoke-SqlReader -ConnectionString (Format-SqlConnectionString -SqlInstance $instance -DatabaseName $database) -CommandText $commandText | ConvertFrom-IDataReader
            } 
        } 
    }
}

# Create an OLEDB connection string with the specified parameters.
function Format-OleDbConnectionString {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)][string]$ProviderName,
        [Parameter(Mandatory=$true)][string]$DataSource,
        [string]$DatabaseName,
        [bool]$IntegrateSecurity = $true,
        [string]$ApplicationName = (Get-CsApplicationName),
        [string]$WorkstationID = (Get-CsWorkstationID),
        [hashtable]$AdditionalValues
    )
    $sb = New-Object System.Data.OleDb.OleDbConnectionStringBuilder
    $sb.Provider = $ProviderName
    $sb["Data Source"] = $DataSource
    if($DatabaseName) { $sb.Database = $DatabaseName; }
    if($IntegrateSecurity) { $sb["Trusted_Connection"] = 'yes'; }
    if($ApplicationName) { $sb["Application Name"] = $ApplicationName; }
    if($WorkstationID) { $sb["Workstation ID"] = $WorkstationID; }
    Write-Verbose "$($MyInvocation.MyCommand.Name) `"$($sb.ToString())`""
    return $sb.ToString()
}

<#
Sample: 
$cs = New-OleDbAceAccess -File 'U:\SampleData\Access1.accdb'
Invoke-DataOleDb -ConnectionString $cs -Query 'SELECT * FROM Table1'
#>
# Create a new OLEDB ACE connection string for Access files
function Format-OleDbConnectionStringAceAccess {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)][string]$File
    )
    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=$File;"
}

<#
Sample:
$cs = New-OleDbAceExcel -File 'U:\SampleData\Excel1.xlsx'
Invoke-DataOleDb -ConnectionString $cs -Query 'SELECT * FROM [Sheet1$]'
#>
# Create a new OLEDB ACE connection string for Excel files
function Format-OleDbConnectionStringAceExcel {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)][string]$File,
        [switch]$HasColumnHeaders,
        [int]$Imex
    )
    $excel = switch -Regex ($File) {
        'xlsx$' {'Excel 12.0 Xml'}
        'xlsm$' {'Excel 12.0 Macro'}
        'xlsb$' {'Excel 12.0'}
        'xls$' {'Excel 8.0'}
        default {'Excel 12.0 Xml'}
    }
    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=$File;Extended Properties=`"$excel;HDR=$(if($HasColumnHeaders){'YES'}else{'NO'})$(if($Imex){";IMEX=$($Imex)"})`";"
}

# Create a SQL connection string with the specified parameters.
function Format-SqlConnectionString {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)][string]$SqlInstance,
        [string]$DatabaseName,
        [bool]$IntegrateSecurity = $true,
        [string]$ApplicationName = (Get-CsApplicationName),
        [string]$WorkstationID = (Get-CsWorkstationID),
        [hashtable]$AdditionalValues
    )
    $sb = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
    $sb["Data Source"] = $SqlInstance
    if($DatabaseName) { $sb["Initial Catalog"] = $DatabaseName; }
    if($IntegrateSecurity) { $sb["Integrated Security"] = $true; }
    if($ApplicationName) { $sb["Application Name"] = $ApplicationName; }
    if($WorkstationID) { $sb["Workstation ID"] = $WorkstationID; }
    Write-Verbose "$($MyInvocation.MyCommand.Name) `"$($sb.ToString())`""
    return $sb.ToString()
}

# Perform a non-query (no data is returned) execution against an OLEDB source. If any data is affected and rowcount is turned on, the number of affected rows are returned.
function Invoke-OleDbNonQuery {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)][string]$ConnectionString,
        [Parameter(Mandatory=$true)][string]$CommandText,
        [int]$CommandTimeout = 300
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        $oleDbNonQueryConnection = New-Object System.Data.OleDb.OleDbConnection $ConnectionString
        $oleDbNonQueryConnection.Open()
        $oleDbNonQueryCommand = $oleDbNonQueryConnection.CreateCommand()
        $oleDbNonQueryCommand.CommandText = $CommandText
        $oleDbNonQueryCommand.CommandTimeout = $CommandTimeout
        Write-Verbose "$($MyInvocation.MyCommand.Name)(CommandTimeout=$($oleDbNonQueryCommand.CommandTimeout)s) on [$($oleDbNonQueryConnection.DataSource)].[$($oleDbNonQueryConnection.Database)]`r`n`t$CommandText"
        $int = $oleDbNonQueryCommand.ExecuteNonQuery()
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"
        $int
    }
    catch { throw; }
    finally {
        if($oleDbNonQueryCommand) { $oleDbNonQueryCommand.Dispose() }
        if($oleDbNonQueryConnection) { $oleDbNonQueryConnection.Dispose() }
    }
}
# Create a DbDataReader (i.e. IDataReader) from an OLEDB source.
function Invoke-OleDbReader {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)][string]$ConnectionString,
        [Parameter(Mandatory=$true)][string]$CommandText,
        [int]$CommandTimeout = 300
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        $oleDbReaderConnection = New-Object System.Data.OleDb.OleDbConnection $ConnectionString
        $oleDbReaderConnection.Open()
        $oleDbReaderCommand = $oleDbReaderConnection.CreateCommand()
        $oleDbReaderCommand.CommandText = $CommandText;
        $oleDbReaderCommand.CommandTimeout = $CommandTimeout
        Write-Verbose "$($MyInvocation.MyCommand.Name)(CommandTimeout=$($oleDbReaderCommand.CommandTimeout)s) on [$($oleDbReaderConnection.DataSource)].[$($oleDbReaderConnection.Database)]`r`n`t$CommandText"
        $oleDbDataReader = $oleDbReaderCommand.ExecuteReader([System.Data.CommandBehavior]::CloseConnection)
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"
        (,$oleDbDataReader)
    }
    catch {
        if($oleDbReaderCommand) { $oleDbReaderCommand.Dispose() }
        if($oleDbReaderConnection) { $oleDbReaderConnection.Dispose() }
        throw;
    }
}
# Perform a scalar execution against an OLEDB source. One value is returned (first row, first column).
function Invoke-OleDbScalar {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true)][string]$ConnectionString,
        [Parameter(Mandatory=$true)][string]$CommandText,
        [int]$CommandTimeout = 300
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        $oleDbScalarConnection = New-Object System.Data.OleDb.OleDbConnection $ConnectionString
        $oleDbScalarConnection.Open()
        $oleDbScalarCommand = $oleDbScalarConnection.CreateCommand()
        $oleDbScalarCommand.CommandText = $CommandText
        $oleDbScalarCommand.CommandTimeout = $CommandTimeout
        Write-Verbose "$($MyInvocation.MyCommand.Name)(CommandTimeout=$($oleDbScalarCommand.CommandTimeout)s) on [$($oleDbScalarConnection.DataSource)].[$($oleDbScalarConnection.Database)]`r`n`t$CommandText"
        $object = $oleDbScalarCommand.ExecuteScalar()
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"
        $object
    }
    catch { throw; }
    finally {
        if($oleDbScalarCommand) { $oleDbScalarCommand.Dispose() }
        if($oleDbScalarConnection) { $oleDbScalarConnection.Dispose() }
    }
}

# Perform a SQL server bulk copy from the specified source (DataTable or IDataReader).
# Returns the number of rows copied.
function Invoke-SqlBulkCopy {
    [CmdletBinding()] param (
        [parameter(ValueFromPipeline=$true)][System.Data.Common.DbDataReader]$DbDataReader,
        [parameter(ValueFromPipeline=$true)][System.Data.IDataReader]$IDataReader,
        [parameter(ValueFromPipeline=$true)][System.Data.DataTable]$DataTable,
        [parameter(ValueFromPipeline=$true)][System.Data.DataRow[]]$DataRows,
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionString")][string]$ConnectionString,
        [Parameter(Mandatory=$true,ParameterSetName='ConnectionParts')][string]$SqlInstance = '(local)',
        [Parameter(Mandatory=$false,ParameterSetName='ConnectionParts')][string]$DatabaseName = 'master',
        [parameter(Mandatory=$true)][string]$SchemaName,
        [parameter(Mandatory=$true)][string]$TableName,
        [int]$BatchSize = 12000,
        [int]$Timeout = 1200,
        [switch]$EnableStreaming = $true
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        if($PsCmdlet.ParameterSetName -eq 'ConnectionParts'){$ConnectionString = Format-SqlConnectionString -SqlInstance $SqlInstance -DatabaseName $DatabaseName}
        $sqlBulkCopyConnection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
        $sqlBulkCopyConnection.Open()
        $sqlBulkCopy = New-Object System.Data.SqlClient.SqlBulkCopy $sqlBulkCopyConnection
        $sqlBulkCopy.BatchSize = $BatchSize
        $sqlBulkCopy.BulkCopyTimeout = $Timeout
        $sqlBulkCopy.DestinationTableName = "[$SchemaName].[$TableName]"
        $sqlBulkCopy.EnableStreaming = $EnableStreaming
        Write-Verbose "$($MyInvocation.MyCommand.Name)(BatchSize=$($sqlBulkCopy.BatchSize),Timeout=$($sqlBulkCopy.BulkCopyTimeout),Streaming=$($sqlBulkCopy.EnableStreaming)) to [$SchemaName].[$TableName] on [$($sqlBulkCopyConnection.DataSource)].[$($sqlBulkCopyConnection.Database)]"
        if($DbDataReader) { $sqlBulkCopy.WriteToServer($DbDataReader) }
        elseif($IDataReader) { $sqlBulkCopy.WriteToServer($IDataReader) }
        elseif($DataTable) { $sqlBulkCopy.WriteToServer($DataTable) }
        elseif($DataRows) { $sqlBulkCopy.WriteToServer($DataRows) }
        else { throw "$($MyInvocation.MyCommand.Name): No supported parameter set found." }
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"

        [System.Data.SqlClient.SqlBulkCopy].GetField('_rowsCopied', [System.Reflection.BindingFlags]::GetField + [System.Reflection.BindingFlags]::Instance + [System.Reflection.BindingFlags]::NonPublic).GetValue($sqlBulkCopy)
    }
    catch { throw; }
    finally {
        if($sqlBulkCopy) {$sqlBulkCopy.Dispose()}
        if($sqlBulkCopyConnection) {$sqlBulkCopyConnection.Dispose()}
        if($DataTable) {$DataTable.Dispose()}
        if($IDataReader) {$IDataReader.Dispose()}
        if($DbDataReader) {$DbDataReader.Dispose()}
    }
}

# Perform a non-query (no data returned) execution. If any data is affected and rowcount is turned on, the number of affected rows are returned.
function Invoke-SqlNonQuery {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionString")][string]$ConnectionString,
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionParts")][string]$SqlInstance = '(local)',
        [Parameter(Mandatory=$false,ParameterSetName="ConnectionParts")][string]$DatabaseName = 'master',
        [Parameter(Mandatory=$true)][string]$CommandText,
        [int]$CommandTimeout = 300
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        if($PsCmdlet.ParameterSetName -eq 'ConnectionParts'){$ConnectionString = Format-SqlConnectionString -SqlInstance $SqlInstance -DatabaseName $DatabaseName}
        $sqlNonQueryConnection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
        $sqlNonQueryConnection.Open()
        $sqlNonQueryCommand = $sqlNonQueryConnection.CreateCommand()
        $sqlNonQueryCommand.CommandText = $CommandText
        $sqlNonQueryCommand.CommandTimeout = $CommandTimeout
        Write-Verbose "$($MyInvocation.MyCommand.Name)(CommandTimeout=$($sqlNonQueryCommand.CommandTimeout)s) on [$($sqlNonQueryConnection.DataSource)].[$($sqlNonQueryConnection.Database)]`r`n`t$CommandText"
        $int = $sqlNonQueryCommand.ExecuteNonQuery()
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"
        $int
    }
    catch { throw; }
    finally {
        if($sqlNonQueryCommand){$sqlNonQueryCommand.Dispose()}
        if($sqlNonQueryConnection){$sqlNonQueryConnection.Dispose()}
    }
}
# Perform an inline non-query (no data returned) execution.
function Invoke-SqlNonQueryAction {
    [CmdletBinding()] param (
        [parameter(Mandatory=$true)][System.Data.IDataReader]$IDataReader,
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionString")][string]$ConnectionString,
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionParts")][string]$SqlInstance = '(local)',
        [Parameter(Mandatory=$false,ParameterSetName="ConnectionParts")][string]$DatabaseName = 'master',
        [parameter(Mandatory=$true)][string]$CommandText,
        [int]$CommandTimeout = 300
    )
    try {
        Write-Verbose "$($MyInvocation.MyCommand.Name) $($PsCmdlet.ParameterSetName)"
        if($PsCmdlet.ParameterSetName -eq 'ConnectionParts'){$ConnectionString = Format-SqlConnectionString -SqlInstance $SqlInstance -DatabaseName $DatabaseName}
        [void](Invoke-SqlNonQuery $ConnectionString -CommandText $CommandText -CommandTimeout $CommandTimeout)

        (,$IDataReader)
    }
	catch { throw; }
}

# Create a sql data reader (IDataReader) from SQL server.
function Invoke-SqlReader {
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionString")][string]$ConnectionString,
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionParts")][string]$SqlInstance = '(local)',
        [Parameter(Mandatory=$false,ParameterSetName="ConnectionParts")][string]$DatabaseName = 'master',
        [Parameter(Mandatory=$true)][string]$CommandText,
        [int]$CommandTimeout = 300
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        if($PsCmdlet.ParameterSetName -eq 'ConnectionParts'){$ConnectionString = Format-SqlConnectionString -SqlInstance $SqlInstance -DatabaseName $DatabaseName}
        $sqlReaderConnection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
        $sqlReaderConnection.Open()
        $sqlReaderCommand = $sqlReaderConnection.CreateCommand()
        $sqlReaderCommand.CommandText = $CommandText;
        $sqlReaderCommand.CommandTimeout = $CommandTimeout
        Write-Verbose "$($MyInvocation.MyCommand.Name)(CommandTimeout=$($sqlReaderCommand.CommandTimeout)s) on [$($sqlReaderConnection.DataSource)].[$($sqlReaderConnection.Database)]`r`n`t$CommandText"
        $sqlDataReader = $sqlReaderCommand.ExecuteReader([System.Data.CommandBehavior]::CloseConnection)
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"
        (,$sqlDataReader)
    }
    catch {
        if($sqlDataReader){$sqlDataReader.Dispose()}
        if($sqlReaderCommand){$sqlReaderCommand.Dispose();}
        if($sqlReaderConnection){$sqlReaderConnection.Dispose();}
        throw;
    }
}

# Perform a scalar execution against SQL server. One value is returned (first row, first column).
function Invoke-SqlScalar
{
    [CmdletBinding()] param (
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionString")][string]$ConnectionString,
        [Parameter(Mandatory=$true,ParameterSetName="ConnectionParts")][string]$SqlInstance = '(local)',
        [Parameter(Mandatory=$false,ParameterSetName="ConnectionParts")][string]$DatabaseName = 'master',
        [Parameter(Mandatory=$true)][string]$CommandText,
        [int]$CommandTimeout = 300
    )
    try {
        $watch = [System.Diagnostics.Stopwatch]::StartNew()
        if($PsCmdlet.ParameterSetName -eq 'ConnectionParts'){$ConnectionString = Format-SqlConnectionString -SqlInstance $SqlInstance -DatabaseName $DatabaseName}
        $sqlScalarConnection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
        $sqlScalarConnection.Open()
        $sqlScalarCommand = $sqlScalarConnection.CreateCommand()
        $sqlScalarCommand.CommandText = $CommandText
        $sqlScalarCommand.CommandTimeout = $CommandTimeout
        Write-Verbose "$($MyInvocation.MyCommand.Name)(CommandTimeout=$($sqlScalarCommand.CommandTimeout)s) on [$($sqlScalarConnection.DataSource)].[$($sqlScalarConnection.Database)]`r`n`t$CommandText"
        $object = $sqlScalarCommand.ExecuteScalar()
        $watch.Stop(); Write-Verbose "$($MyInvocation.MyCommand.Name) #$($watch.Elapsed)"
        $object
    }
    catch { throw; }
    finally {
        if($sqlScalarCommand){$sqlScalarCommand.Dispose()}
        if($sqlScalarConnection){$sqlScalarConnection.Dispose()}
    }
}

# If the table does not exist, create it from either the SQL server schema table (if the data is coming from SQL) or by defaults from the .NET CLR data types.
function Invoke-SqlCreateTableIfNotExists
{
    [CmdletBinding()] param (
        [parameter(Mandatory=$true,ValueFromPipeline=$true)][System.Data.IDataReader]$IDataReader,
        [parameter(Mandatory=$true)][string]$ConnectionString,
        [parameter(Mandatory=$true)][string]$SchemaName,
        [parameter(Mandatory=$true)][string]$TableName,
        [string[]]$ColumnDefinitions
    )
    try
    {
        $schemaTable = $IDataReader.GetSchemaTable();

        if($ColumnDefinitions) {
            $createTable = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES (NOLOCK) WHERE TABLE_SCHEMA = '$SchemaName' AND TABLE_NAME = '$TableName') BEGIN CREATE TABLE [$SchemaName].[$TableName] ($($ColumnDefinitions -join ",")) END;"
        }
        elseif($schemaTable.Columns.Contains('AllowDBNull') -and $schemaTable.Columns.Contains('ColumnName') -and $schemaTable.Columns.Contains('ColumnSize') -and $schemaTable.Columns.Contains('DataTypeName') -and $schemaTable.Columns.Contains('NumericPrecision') -and $schemaTable.Columns.Contains('NumericScale')) {
            $createTable = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES (NOLOCK) WHERE TABLE_SCHEMA = '$SchemaName' AND TABLE_NAME = '$TableName') BEGIN CREATE TABLE [$SchemaName].[$TableName] (
                $(@(foreach($dataRow in $schemaTable.Rows) {
                    $allowDBNull = if($dataRow.AllowDBNull -eq $true) {'NULL'} else {'NOT NULL'}
                    $columnName = if([string]::IsNullOrWhiteSpace($dataRow.ColumnName)) {"Column_$($dataRow.ColumnOrdinal)"} else {$dataRow.ColumnName}
                    $columnSize = $dataRow.ColumnSize
                    [string]$dataTypeName = $dataRow.DataTypeName
                    $dataTypeArgument = ''
                    switch ($dataTypeName) {
                        {($_ -eq 'rowversion') -or($_ -eq 'timestamp')} {$dataTypeName = 'varbinary'; $dataTypeArgument = '(8)'}
                        {($_ -eq 'binary') -or ($_ -eq 'varbinary') -or ($_ -eq 'char') -or ($_ -eq 'nchar') -or ($_ -eq 'nvarchar') -or ($_ -eq 'varchar')} { $dataTypeArgument = if($dataRow.ColumnSize -eq -1 -or $dataRow.ColumnSize -eq 2147483647 -or $dataRow.ColumnSize -eq 1073741823) {'(MAX)'} else {"($($dataRow.ColumnSize))"} } # 2147483647 from SqlClient and 1073741823 from OleDb/Odbc.
                        {($_ -eq 'decimal') -or ($_ -eq 'numeric')} { $dataTypeArgument = "($($dataRow.NumericPrecision),$($dataRow.NumericScale))" }
                        {($_ -eq 'datetime2') -or ($_ -eq 'datetimeoffset') -or ($_ -eq 'time')} { $dataTypeArgument = "($($dataRow.NumericScale))" }
                        (([string]$_).LastIndexOf('.') -gt -1) { $dataTypeName = $dataTypeName.Substring(([string]$_).LastIndexOf('.') + 1) }
                    }

                    "[$columnName] [$dataTypeName]$dataTypeArgument $allowDBNull"
                }) -join ',')
            ) END;"
        }
        else {
            $createTable = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES (NOLOCK) WHERE TABLE_SCHEMA = '$SchemaName' AND TABLE_NAME = '$TableName') BEGIN CREATE TABLE [$SchemaName].[$TableName] (
                $(@(foreach($dataRow in $schemaTable.Rows) {
                    $columnName = if([string]::IsNullOrWhiteSpace($dataRow.ColumnName)) {"Column_$($dataRow.ColumnOrdinal)"} else {$dataRow.ColumnName}
                    [type]$dataType = if($dataRow.DataType -eq [dbnull]::Value) {$null} else {$dataRow.DataType}
                    #Determin the SQL data type name for the column.
                    if($dataType.Name -eq 'boolean') {$dataTypeName = 'bit'}
                    elseif($dataType.Name -eq 'byte') {$dataTypeName = 'tinyint'}
                    elseif($dataType.Name -eq 'byte[]') {$dataTypeName = 'varbinary'}
                    elseif($dataType.Name -eq 'datetime') {$dataTypeName = 'datetime'}
                    elseif($dataType.Name -eq 'datetimeoffset') {$dataTypeName = 'datetimeoffset'}
                    elseif($dataType.Name -eq 'decimal') {$dataTypeName = 'decimal'}
                    elseif($dataType.Name -eq 'double') {$dataTypeName = 'float'}
                    elseif($dataType.Name -eq 'guid') {$dataTypeName = 'uniqueidentifier'}
                    elseif($dataType.Name -eq 'int16') {$dataTypeName = 'smallint'}
                    elseif($dataType.Name -eq 'int32') {$dataTypeName = 'int'}
                    elseif($dataType.Name -eq 'int64') {$dataTypeName = 'bigint'}
                    elseif($dataType.Name -eq 'sbyte') {$dataTypeName = 'tinyint'}
                    elseif($dataType.Name -eq 'sbyte[]') {$dataTypeName = 'varbinary'}
                    elseif($dataType.Name -eq 'single') {$dataTypeName = 'real'}
                    elseif($dataType.Name -eq 'string') {$dataTypeName = 'nvarchar'}
                    elseif($dataType.Name -eq 'uint16') {$dataTypeName = 'smallint'}
                    elseif($dataType.Name -eq 'uint32') {$dataTypeName = 'int'}
                    elseif($dataType.Name -eq 'uint64') {$dataTypeName = 'bigint'}
                    elseif($dataType.Name -eq 'object') {$dataTypeName = 'sql_variant'}
                    # Determin the SQL data type argument for the column.
                    if($dataTypeName -eq 'binary' -or $dataTypeName -eq 'char') {$dataTypeArgument = '(8000)'}
                    elseif($dataTypeName -eq 'datetime2' -or $dataTypeName -eq 'datetimeoffset' -or $dataTypeName -eq 'time') {$dataTypeArgument = '(7)'}
                    elseif($dataTypeName -eq 'decimal' -or $dataTypeName -eq 'numeric') {$dataTypeArgument = '(38,19)'}
                    elseif($dataTypeName -eq 'nchar') {$dataTypeArgument = '(4000)'}
                    elseif($dataTypeName -eq 'nvarchar' -or $dataTypeName -eq 'varbinary' -or $dataTypeName -eq 'varchar') {$dataTypeArgument = '(MAX)'}
                    else {$dataTypeArgument = ''}

                    "[$columnName] [$dataTypeName]$dataTypeArgument NULL"
                }) -join ',')
            ) END;"
        }

        $schemaTable.Dispose();

        Write-Verbose "$($MyInvocation.MyCommand.Name) [$SchemaName].[$TableName]"
        [void](Invoke-SqlNonQuery $ConnectionString -CommandText $createTable)

        (,$IDataReader)
    }
	catch { throw; }
}

# Drop the SQL table if it exists.
function Invoke-SqlDropTableIfExists
{
    [CmdletBinding()] param (
        [parameter(Mandatory=$true,ValueFromPipeline=$true)][System.Data.IDataReader]$IDataReader,
        [parameter(Mandatory=$true)][string]$ConnectionString,
        [parameter(Mandatory=$true)][string]$SchemaName,
        [parameter(Mandatory=$true)][string]$TableName,
        [int]$CommandTimeout = 300
    )
    try {
        Write-Verbose "$($MyInvocation.MyCommand.Name) on [$SchemaName].[$TableName]"
        [void](Invoke-SqlNonQuery $ConnectionString -CommandText "DROP TABLE IF EXISTS [$SchemaName].[$TableName];" -CommandTimeout $CommandTimeout)

        (,$IDataReader)
    }
	catch { throw; }
}

# Truncate the SQL table.
function Invoke-SqlTruncateTable
{
    [CmdletBinding()] param (
        [parameter(Mandatory=$true,ValueFromPipeline=$true)][System.Data.IDataReader]$IDataReader,
        [parameter(Mandatory=$true)][string]$ConnectionString,
        [parameter(Mandatory=$true)][string]$SchemaName,
        [parameter(Mandatory=$true)][string]$TableName,
        [int]$CommandTimeout = 300
    )
    try {
        Write-Verbose "$($MyInvocation.MyCommand.Name) on [$SchemaName].[$TableName]"
        [void](Invoke-SqlNonQuery $ConnectionString -CommandText "TRUNCATE TABLE [$SchemaName].[$TableName];" -CommandTimeout $CommandTimeout)

        (,$IDataReader)
    }
	catch { throw; }
}

function Measure-SqlTable
{
    [CmdletBinding()] param (
        [parameter(Mandatory=$true)][string]$SqlInstance,
        [parameter(Mandatory=$true)][string]$DatabaseName,
        [parameter(Mandatory=$true)][string]$SchemaName,
        [parameter(Mandatory=$true)][string]$TableName,
        [int]$CommmandTimeout = 300
    )
    $connectionString = Format-SqlConnectionString -SqlInstance $SqlInstance -DatabaseName $DatabaseName
    $commandText = "
SELECT
    CONVERT(sysname, SERVERPROPERTY('servername')) AS [SqlInstance]
    ,TABLE_CATALOG AS [DatabaseName]
    ,TABLE_SCHEMA AS [SchemaName]
    ,TABLE_NAME AS [TableName]
    ,(SELECT COUNT(*) FROM [$SchemaName].[$TableName] (NOLOCK)) AS [TableCount]
    ,GETDATE() AS [Timestamp]
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = '$SchemaName' AND TABLE_NAME = '$TableName' 
"
    Write-Verbose "$($MyInvocation.MyCommand.Name) on [$SqlInstance].[$DatabaseName].[$SchemaName].[$TableName]"
    $dataTable = Invoke-SqlReader -ConnectionString $connectionString -CommandText $commandText | Convert-IDataReaderToDataTable
    
    (,$dataTable)
}

function Measure-SqlTableColumns
{
    [CmdletBinding()] param (
        [parameter(Mandatory=$true)][string]$SqlInstance,
        [parameter(Mandatory=$true)][string]$DatabaseName,
        [parameter(Mandatory=$true)][string]$SchemaName,
        [parameter(Mandatory=$true)][string]$TableName,
        [int]$CommmandTimeout = 300
    )
    Write-Verbose "$($MyInvocation.MyCommand.Name) on [$SqlInstance].[$DatabaseName].[$SchemaName].[$TableName]"
    $connectionString = Format-SqlConnectionString -SqlInstance $SqlInstance -DatabaseName $DatabaseName
    $commandText = "
SELECT
    CONVERT([sysname], SERVERPROPERTY('servername')) AS [SqlInstance]
    ,[TABLE_CATALOG] AS [DatabaseName]
    ,[TABLE_SCHEMA] AS [SchemaName]
    ,[TABLE_NAME] AS [TableName]
    ,[COLUMN_NAME] AS [ColumnName]
    ,[ORDINAL_POSITION] AS ColumnOrdinal
    ,[DATA_TYPE] AS [ColumnDataTypeName]
    ,CASE WHEN [DATA_TYPE] IN ('date','datetime2','datetimeoffset') THEN '(' + CAST([DATETIME_PRECISION] AS [nvarchar](128)) + ')' WHEN [DATA_TYPE] IN ('decimal','numeric') THEN '(' + CAST([NUMERIC_PRECISION] AS [nvarchar](128)) + ',' + CAST([NUMERIC_SCALE] AS [nvarchar](128)) + ')' ELSE '' END AS [ColumnDataTypeArguments]
    ,CASE [IS_NULLABLE] WHEN 'NO' THEN 'NOT NULL' ELSE 'NULL' END AS [ColumnNullability]
FROM [INFORMATION_SCHEMA].[COLUMNS] 
WHERE [TABLE_SCHEMA] = '$SchemaName' AND [TABLE_NAME] = '$TableName' 
"
    $dataTable = Invoke-SqlReader -ConnectionString $connectionString -CommandText $commandText | Convert-IDataReaderToDataTable

    [void]($dataTable.Columns.Add('ColumnCountValues', [System.Int64]))
    [void]($dataTable.Columns.Add('ColumnDataLength', [System.Int64]))
    [void]($dataTable.Columns.Add('ColumnMaxValue', [System.Object]))
    [void]($dataTable.Columns.Add('ColumnMinValue', [System.Object]))
    [void]($dataTable.Columns.Add('Timestamp', [System.DateTime]))
    $aggregateColumns = @(foreach($row in $dataTable.Rows)
    {
        $columnName = $row['ColumnName'];
        $columnDataTypeName = $row['ColumnDataTypeName'];
        #$columnDataTypeArguments = $row['ColumnDataTypeArguments'];

        if($row['ColumnNullability'] -eq 'NULL' -and ('image','ntext','text') -notcontains $columnDataTypeName) {
            "COUNT([$columnName]) AS [$($columnName)_COUNT]"
        }
        if($row['ColumnNullability'] -eq 'NULL' -and ('ntext','text') -contains $columnDataTypeName) {
            "COUNT(CAST([$columnName] AS [nvarchar](max))) AS [$($columnName)_COUNT]"
            "MAX(DATALENGTH(CAST([$columnName] AS [nvarchar](max)))) AS [$($columnName)_DATALENGTH]"
        }
        if('bigint','decimal','float','int','numeric','real','smallint','tinyint' -contains $columnDataTypeName) {
            "MAX([$columnName]) AS [$($columnName)_MAX]"
            "MIN([$columnName]) AS [$($columnName)_MIN]"
        }
        if('bit' -contains $columnDataTypeName) {
            "MAX(CAST([$columnName] AS [int])) AS [$($columnName)_MAX]"
            "MIN(CAST([$columnName] AS [int])) AS [$($columnName)_MIN]"
        }
        if('date','datetime','time' -contains $columnDataTypeName) {
            # datetime2 supports a larger range than SqlDateTime, and can therefore generate errors in a DataTable, datetimeoffset likely falls under this as well, but is unclear at this point #
            "MAX([$columnName]) AS [$($columnName)_MAX]"
            "MIN([$columnName]) AS [$($columnName)_MIN]"
        }
        if('nvarchar','varchar' -contains $columnDataTypeName) {
            "MAX(DATALENGTH([$columnName])) AS [$($columnName)_DATALENGTH]"
        }
    })

    if($aggregateColumns.Count -gt 0) {
        $commandText = "
SELECT
    -- The $($dataTable.Rows.Count) columns generated $($aggregateColumns.Count) aggregations.
    $(($aggregateColumns + "GETDATE() AS [Timestamp]") -join "`r`n`t,")
FROM [$SchemaName].[$TableName]
"
        $aggValues = Invoke-SqlReader -ConnectionString $connectionString -CommandText $commandText | ConvertFrom-IDataReader
        foreach($row in $dataTable.Rows) {
            $columnName = $row['ColumnName'];
            if($null -ne $aggValues."$($columnName)_COUNT") {$row['ColumnCountValues'] = $aggValues."$($columnName)_COUNT"}
            if($null -ne $aggValues."$($columnName)_DATALENGTH") {$row['ColumnDataLength'] = $aggValues."$($columnName)_DATALENGTH"}
            if($null -ne $aggValues."$($columnName)_MAX") {$row['ColumnMaxValue'] = $aggValues."$($columnName)_MAX"}
            if($null -ne $aggValues."$($columnName)_MIN") {$row['ColumnMinValue'] = $aggValues."$($columnName)_MIN"}
            $row['Timestamp'] = $aggValues.'Timestamp'
        }
    }
    else {
        foreach($row in $dataTable.Rows) {
            $row['Timestamp'] = Invoke-SqlScalar -ConnectionString $connectionString -CommandText 'SELECT GETDATE() AS [Timestamp]'
        }
    }

    (,$dataTable)
}
