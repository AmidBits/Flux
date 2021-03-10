namespace Flux.Model.RulesEngineExample
{
	public class User
	{
		public int Age { get; set; }
		public string Name { get; set; }
		public string BirthCountry { get; set; }

		public User(int age, string name, string birthCountry)
		{
			Age = age;
			Name = name;
			BirthCountry = birthCountry;
		}

		public override string ToString()
			=> $"<{nameof(User)}: {Name}, {Age} ({BirthCountry})>";

		public static void ShowCase()
		{
			var rules = new Flux.Model.RulesDictionary();

			rules.Add("AgeLimit", new Flux.Model.Rule("Age", "GreaterThan", 20));
			rules.Add("NameRequirement", new Flux.Model.Rule("Name", "Equal", "John"));
			rules.Add("CountryOfBirth", new Flux.Model.Rule("BirthCountry", "Equal", "Canada"));

			var rulesCompiled = rules.CompileRules<User>();
			foreach (var rule in rules)
				System.Console.WriteLine(rule);

			var user1 = new User(43, "Royi", "Australia");
			var user2 = new User(33, "John", "England");
			var user3 = new User(23, "John", "Canada");

			rulesCompiled.EvaluateRules(user1).Dump();
			rulesCompiled.EvaluateRules(user2).Dump();
			rulesCompiled.EvaluateRules(user3).Dump();
		}
	}
}
