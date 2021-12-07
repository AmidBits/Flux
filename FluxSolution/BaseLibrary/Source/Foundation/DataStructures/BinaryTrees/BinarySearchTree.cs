namespace Flux.Collections.Generic
{
  public interface IBinarySearchTreeNode<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public bool IsEmpty { get; }

    public TKey Key { get; }
    public TValue Value { get; }

    public IBinarySearchTreeNode<TKey, TValue> Left { get; }
    public IBinarySearchTreeNode<TKey, TValue> Right { get; }
  }

#if NET5_0
  public sealed class BinarySearchTreeNode<TKey, TValue>
#else
  public record class BinarySearchTreeNode<TKey, TValue>
#endif
    : IBinarySearchTreeNode<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public bool IsEmpty
      => false;

    public TKey Key { get; }
    public TValue Value { get; }

    public IBinarySearchTreeNode<TKey, TValue> Left { get; }
    public IBinarySearchTreeNode<TKey, TValue> Right { get; }

    public BinarySearchTreeNode(TKey name, TValue value, IBinarySearchTreeNode<TKey, TValue> left, IBinarySearchTreeNode<TKey, TValue> right)
    {
      Key = name;
      Value = value;
      Left = left;
      Right = right;
    }
    public BinarySearchTreeNode(TKey name, TValue value)
      : this(name, value, new BinarySearchTreeEmptyNode<TKey, TValue>(), new BinarySearchTreeEmptyNode<TKey, TValue>())
    { }
  }
#if NET5_0
  public sealed class BinarySearchTreeEmptyNode<TKey, TValue>
#else
  public record class BinarySearchTreeEmptyNode<TKey, TValue>
#endif
    : IBinarySearchTreeNode<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public bool IsEmpty
      => true;

    public TKey Key
      => throw new System.Exception(GetType().Name);
    public TValue Value
      => throw new System.Exception(GetType().Name);

    public IBinarySearchTreeNode<TKey, TValue> Left
      => throw new System.Exception(GetType().Name);
    public IBinarySearchTreeNode<TKey, TValue> Right
      => throw new System.Exception(GetType().Name);
  }

  public sealed class BinarySearchTree<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    // Implements:

    // count()
    // clear()
    // insert()
    // delete()
    // findSymbol()
    //
    // Usage:
    //
    //  TBinarySTree bt = new TBinarySTree();
    //  bt.insert ("Bill", "3.14");
    //  bt.insert ("John". 2.71");
    //  etc.
    //  node = bt.findSymbol ("Bill");
    //  WriteLine ("Node value = {0}\n", node.value);
    //

    private IBinarySearchTreeNode<TKey, TValue> m_root;
    private int m_count = 0;

    public BinarySearchTree()
    {
      m_root = new BinarySearchTreeEmptyNode<TKey, TValue>();
      m_count = 0;
    }

    /// <summary>
    /// Clear the binary tree.
    /// </summary>
    public void Clear()
    {
      KillTree(m_root);

      m_count = 0;

      // Recursive destruction of binary search tree, called by method clear and destroy. Can be used to kill a sub-tree of a larger tree.
      // This is a hanger on from its Delphi origins, it might be dispensable given the garbage collection abilities of .NET
      static void KillTree(IBinarySearchTreeNode<TKey, TValue> node)
      {
        if (!node.IsEmpty)
        {
          KillTree(node.Left);
          KillTree(node.Right);
        }
      }
    }

    /// <summary>
    /// Returns the number of nodes in the tree
    /// </summary>
    /// <returns>Number of nodes in the tree</returns>
    public int Count()
      => m_count;

    /// <summary>
    /// Find name in tree. Return a reference to the node
    /// if symbol found else return null to indicate failure.
    /// </summary>
    /// <param name="key">Name of node to locate</param>
    /// <returns>Returns null if it fails to find the node, else returns reference to node</returns>
    public IBinarySearchTreeNode<TKey, TValue> Search(TKey key)
    {
      IBinarySearchTreeNode<TKey, TValue> node = m_root;

      while (!node.IsEmpty)
      {
        switch (key.CompareTo(node.Key))
        {
          case 0:
            return node; // Found.
          case int lt when lt < 0:
            node = node.Left;
            break;
          case int gt when gt > 0:
            node = node.Right;
            break;
        }
      }

      return new BinarySearchTreeEmptyNode<TKey, TValue>();
    }

    // Recursively locates an empty slot in the binary tree and inserts the node
    private void Insert(IBinarySearchTreeNode<TKey, TValue> node, IBinarySearchTreeNode<TKey, TValue> tree)
    {
      if (tree.IsEmpty)
        tree = node;
      else
      {
        switch (node.Key.CompareTo(tree.Key))
        {
          case int lt when lt < 0:
            Insert(node, tree.Left);
            break;
          case int gt when gt > 0:
            Insert(node, tree.Right);
            break;
        }
      }
    }

    /// <summary>Add a symbol to the tree if it's a new one. Returns reference to the new node if a new node inserted, else returns null to indicate node already present.</summary>
    /// <param name="name">Name of node to add to tree</param>
    /// <param name="d">Value of node</param>
    /// <returns> Returns reference to the new node is the node was inserted.
    /// If a duplicate node (same name was located then returns null</returns>
    public IBinarySearchTreeNode<TKey, TValue> Insert(TKey name, TValue d)
    {
      var node = new BinarySearchTreeNode<TKey, TValue>(name, d);

      try
      {
        if (m_root.IsEmpty)
          m_root = node;
        else
          Insert(node, m_root);

        m_count++;

        return node;
      }
      catch (System.Exception) { }

      return new BinarySearchTreeEmptyNode<TKey, TValue>();
    }

    // Searches for a node with name key, name. If found it returns a reference to the node and to thenodes parent. Else returns null.
    private IBinarySearchTreeNode<TKey, TValue> FindParent(TKey name)
    {
      var np = m_root;

      while (!np.IsEmpty)
      {
        switch (name.CompareTo(np.Key))
        {
          case 0:
            return np;
          case int lt when lt < 0:
            np = np.Left;
            break;
          case int gt when gt > 0:
            np = np.Right;
            break;
        }
      }

      return new BinarySearchTreeEmptyNode<TKey, TValue>(); // Not found.
    }

    /// <summary>Find the next ordinal node starting at node startNode. Due to the structure of a binary search tree, the successor node is simply the left most node on the right branch.</summary>
    /// <param name="startNode">Name key to use for searching</param>
    /// <param name="parent">Returns the parent node if search successful</param>
    /// <returns>Returns a reference to the node if successful, else null</returns>
    public IBinarySearchTreeNode<TKey, TValue> FindSuccessor(IBinarySearchTreeNode<TKey, TValue> startNode, ref IBinarySearchTreeNode<TKey, TValue> parent)
    {
      parent = startNode;
      startNode = startNode.Right; // Look for the left-most node on the right side.

      while (!startNode.Left.IsEmpty)
      {
        parent = startNode;
        startNode = startNode.Left;
      }

      return startNode;
    }

    /// <summary>
    /// Delete a given node. This is the more complex method in the binary search
    /// class. The method considers three senarios, 1) the deleted node has no
    /// children; 2) the deleted node as one child; 3) the deleted node has two
    /// children. Case one and two are relatively simple to handle, the only
    /// unusual considerations are when the node is the root node. Case 3) is
    /// much more complicated. It requires the location of the successor node.
    /// The node to be deleted is then replaced by the sucessor node and the
    /// successor node itself deleted. Throws an exception if the method fails
    /// to locate the node for deletion.
    /// </summary>
    /// <param name="key">Name key of node to delete</param>
    public void Delete(TKey key)
    {
      IBinarySearchTreeNode<TKey, TValue> parent = new BinarySearchTreeEmptyNode<TKey, TValue>();

      var nodeToDelete = FindParent(key);

      if (nodeToDelete.IsEmpty)
        throw new System.Exception("Unable to delete node: " + key.ToString());  // can't find node, then say so 

      // Three cases to consider, leaf, one child, two children

      // If it is a simple leaf then just null what the parent is pointing to
      if (nodeToDelete.Left.IsEmpty && nodeToDelete.Right.IsEmpty)
      {
        if (parent.IsEmpty)
        {
          m_root = new BinarySearchTreeEmptyNode<TKey, TValue>();

          return;
        }

        // find out whether left or right is associated 
        // with the parent and null as appropriate
        if (parent.Left == nodeToDelete)
          parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, new BinarySearchTreeEmptyNode<TKey, TValue>(), parent.Right);
        else
          parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, parent.Left, new BinarySearchTreeEmptyNode<TKey, TValue>());

        m_count--;
        return;
      }

      // The left child is empty, so delete the node and move child up.
      if (nodeToDelete.Left.IsEmpty)
      {
        // Special case if we're at the root
        if (parent.IsEmpty)
        {
          m_root = nodeToDelete.Right;
          return;
        }

        // Identify the child and point the parent at the child
        if (parent.Left == nodeToDelete)
          parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, parent.Left, nodeToDelete.Right);
        else
          parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, nodeToDelete.Right, parent.Right);

        m_count--;
        return;
      }

      // The right child is empty, so delete the node and move child up.
      if (nodeToDelete.Right.IsEmpty)
      {
        // Special case if we're at the root			
        if (parent.IsEmpty)
        {
          m_root = nodeToDelete.Left;
          return;
        }

        // Identify the child and point the parent at the child
        if (parent.Left == nodeToDelete)
          parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, nodeToDelete.Left, parent.Right);
        else
          parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, parent.Left, nodeToDelete.Left);

        m_count--;
        return;
      }

      // Both children have nodes, therefore find the successor, replace deleted node with successor and remove successor.
      // The parent argument becomes the parent of the successor.
      IBinarySearchTreeNode<TKey, TValue> successor = FindSuccessor(nodeToDelete, ref parent);
      // Make a copy of the successor node.
      IBinarySearchTreeNode<TKey, TValue> tmp = new BinarySearchTreeNode<TKey, TValue>(successor.Key, successor.Value);
      // Find out which side the successor parent is pointing to the successor and remove the successor.
      if (parent.Left == successor)
        parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, new BinarySearchTreeEmptyNode<TKey, TValue>(), parent.Right);
      else
        parent = new BinarySearchTreeNode<TKey, TValue>(parent.Key, parent.Value, parent.Left, new BinarySearchTreeEmptyNode<TKey, TValue>());

      // Copy over the successor values to the deleted node position
      nodeToDelete = new BinarySearchTreeNode<TKey, TValue>(tmp.Key, tmp.Value, nodeToDelete.Left, nodeToDelete.Right);
      m_count--;
    }

    // Simple 'drawing' routines
    //private string drawNode(IBinarySearchTreeNode<TKey, TValue> node)
    //{
    //  if (node == null)
    //    return "empty";

    //  if ((node.Left == null) && (node.Right == null))
    //    return node.Name;
    //  if ((node.Left != null) && (node.Right == null))
    //    return node.Name + "(" + drawNode(node.Left) + ", _)";

    //  if ((node.Right != null) && (node.Left == null))
    //    return node.Name + "(_, " + drawNode(node.Right) + ")";

    //  return node.Name + "(" + drawNode(node.Left) + ", " + drawNode(node.Right) + ")";
    //}

    /// <summary>
    /// Return the tree depicted as a simple string, useful for debugging, eg
    /// 50(40(30(20, 35), 45(44, 46)), 60)
    /// </summary>
    /// <returns>Returns the tree</returns>
    //public string drawTree()
    //{
    //  return drawNode(root);
    //}
  }
}

//namespace Flux.Collections.Generic
//{
//  /// <summary>
//  /// Represents a binary search tree.  A binary search tree is a binary tree whose nodes are arranged
//  /// such that for any given node k, all nodes in k's left subtree have a value less than k, and all
//  /// nodes in k's right subtree have a value greater than k.
//  /// </summary>
//  /// <typeparam name="T">The type of data stored in the binary tree nodes.</typeparam>
//  public class BinarySearchTree<T>
//  : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>
//  {
//    #region Private Member Variables
//    private BinaryTreeNode<T> root = null;
//    private int count = 0;
//    private System.Collections.Generic.IComparer<T> comparer = System.Collections.Generic.Comparer<T>.Default;    // used to compare node values when percolating down the tree
//    #endregion

//    #region Constructors
//    public BinarySearchTree() { }
//    public BinarySearchTree(System.Collections.Generic.IComparer<T> comparer)
//    {
//      this.comparer = comparer;
//    }
//    #endregion

//    #region Public Methods
//    #region Clear
//    /// <summary>
//    /// Removes the contents of the BST
//    /// </summary>
//    public void Clear()
//    {
//      root = null;
//      count = 0;
//    }
//    #endregion

//    #region CopyTo
//    /// <summary>
//    /// Copies the contents of the BST to an appropriately-sized array of type T, using the Inorder
//    /// traversal method.
//    /// </summary>
//    public void CopyTo(T[] array, int index)
//    {
//      CopyTo(array, index, TraversalMethod.InOrder);
//    }

//    /// <summary>
//    /// Copies the contents of the BST to an appropriately-sized array of type T, using a specified
//    /// traversal method.
//    /// </summary>
//    public void CopyTo(T[] array, int index, TraversalMethod TraversalMethod)
//    {
//      System.Collections.Generic.IEnumerable<T> enumProp = null;

//      // Determine which Enumerator-returning property to use, based on the TraversalMethod input parameter
//      switch (TraversalMethod)
//      {
//        case TraversalMethod.PreOrder:
//          enumProp = Preorder;
//          break;

//        case TraversalMethod.InOrder:
//          enumProp = Inorder;
//          break;

//        case TraversalMethod.PostOrder:
//        default:
//          enumProp = Postorder;
//          break;
//      }

//      // dump the contents of the tree into the passed-in array
//      int i = 0;
//      foreach (T value in enumProp)
//      {
//        array[i + index] = value;
//        i++;
//      }
//    }
//    #endregion

//    #region Add
//    /// <summary>
//    /// Adds a new value to the BST.
//    /// </summary>
//    /// <param name="data">The data to insert into the BST.</param>
//    /// <remarks>Adding a value already in the BST has no effect; that is, the SkipList is not
//    /// altered, the Add() method simply exits.</remarks>
//    public virtual void Add(T data)
//    {
//      // create a new Node instance
//      BinaryTreeNode<T> n = new BinaryTreeNode<T>(data);
//      int result;

//      // now, insert n into the tree
//      // trace down the tree until we hit a NULL
//      BinaryTreeNode<T> current = root, parent = null;
//      while (current != null)
//      {
//        result = comparer.Compare(current.Value, data);
//        if (result == 0)
//          // they are equal - attempting to enter a duplicate - do nothing
//          return;
//        else if (result > 0)
//        {
//          // current.Value > data, must add n to current's left subtree
//          parent = current;
//          current = current.Left;
//        }
//        else if (result < 0)
//        {
//          // current.Value < data, must add n to current's right subtree
//          parent = current;
//          current = current.Right;
//        }
//      }

//      // We're ready to add the node!
//      count++;
//      if (parent == null)
//        // the tree was empty, make n the root
//        root = n;
//      else
//      {
//        result = comparer.Compare(parent.Value, data);
//        if (result > 0)
//          // parent.Value > data, therefore n must be added to the left subtree
//          parent.Left = n;
//        else
//          // parent.Value < data, therefore n must be added to the right subtree
//          parent.Right = n;
//      }
//    }
//    #endregion

//    #region Contains
//    /// <summary>
//    /// Returns a Boolean, indicating if a specified value is contained within the BST.
//    /// </summary>
//    /// <param name="data">The data to search for.</param>
//    /// <returns>True if data is found in the BST; false otherwise.</returns>
//    public bool Contains(T data)
//    {
//      // search the tree for a node that contains data
//      BinaryTreeNode<T> current = root;
//      int result;
//      while (current != null)
//      {
//        result = comparer.Compare(current.Value, data);
//        if (result == 0)
//          // we found data
//          return true;
//        else if (result > 0)
//          // current.Value > data, search current's left subtree
//          current = current.Left;
//        else if (result < 0)
//          // current.Value < data, search current's right subtree
//          current = current.Right;
//      }

//      return false;       // didn't find data
//    }
//    #endregion

//    #region Remove
//    /// <summary>
//    /// Attempts to remove the specified data element from the BST.
//    /// </summary>
//    /// <param name="data">The data to remove from the BST.</param>
//    /// <returns>True if the element is found in the tree, and removed; false if the element is not
//    /// found in the tree.</returns>
//    public bool Remove(T data)
//    {
//      // first make sure there exist some items in this tree
//      if (root == null)
//        return false;       // no items to remove

//      // Now, try to find data in the tree
//      BinaryTreeNode<T> current = root, parent = null;
//      int result = comparer.Compare(current.Value, data);
//      while (result != 0)
//      {
//        if (result > 0)
//        {
//          // current.Value > data, if data exists it's in the left subtree
//          parent = current;
//          current = current.Left;
//        }
//        else if (result < 0)
//        {
//          // current.Value < data, if data exists it's in the right subtree
//          parent = current;
//          current = current.Right;
//        }

//        // If current == null, then we didn't find the item to remove
//        if (current == null)
//          return false;
//        else
//          result = comparer.Compare(current.Value, data);
//      }

//      // At this point, we've found the node to remove
//      count--;

//      // We now need to "rethread" the tree
//      // CASE 1: If current has no right child, then current's left child becomes
//      //         the node pointed to by the parent
//      if (current.Right == null)
//      {
//        if (parent == null)
//          root = current.Left;
//        else
//        {
//          result = comparer.Compare(parent.Value, current.Value);
//          if (result > 0)
//            // parent.Value > current.Value, so make current's left child a left child of parent
//            parent.Left = current.Left;
//          else if (result < 0)
//            // parent.Value < current.Value, so make current's left child a right child of parent
//            parent.Right = current.Left;
//        }
//      }
//      // CASE 2: If current's right child has no left child, then current's right child
//      //         replaces current in the tree
//      else if (current.Right.Left == null)
//      {
//        current.Right.Left = current.Left;

//        if (parent == null)
//          root = current.Right;
//        else
//        {
//          result = comparer.Compare(parent.Value, current.Value);
//          if (result > 0)
//            // parent.Value > current.Value, so make current's right child a left child of parent
//            parent.Left = current.Right;
//          else if (result < 0)
//            // parent.Value < current.Value, so make current's right child a right child of parent
//            parent.Right = current.Right;
//        }
//      }
//      // CASE 3: If current's right child has a left child, replace current with current's
//      //          right child's left-most descendent
//      else
//      {
//        // We first need to find the right node's left-most child
//        BinaryTreeNode<T> leftmost = current.Right.Left, lmParent = current.Right;
//        while (leftmost.Left != null)
//        {
//          lmParent = leftmost;
//          leftmost = leftmost.Left;
//        }

//        // the parent's left subtree becomes the leftmost's right subtree
//        lmParent.Left = leftmost.Right;

//        // assign leftmost's left and right to current's left and right children
//        leftmost.Left = current.Left;
//        leftmost.Right = current.Right;

//        if (parent == null)
//          root = leftmost;
//        else
//        {
//          result = comparer.Compare(parent.Value, current.Value);
//          if (result > 0)
//            // parent.Value > current.Value, so make leftmost a left child of parent
//            parent.Left = leftmost;
//          else if (result < 0)
//            // parent.Value < current.Value, so make leftmost a right child of parent
//            parent.Right = leftmost;
//        }
//      }

//      // Clear out the values from current
//      current.Left = current.Right = null;
//      current = null;

//      return true;
//    }
//    #endregion

//    #region GetEnumerator
//    /// <summary>
//    /// Enumerates the BST's contents using inorder traversal.
//    /// </summary>
//    /// <returns>An enumerator that provides inorder access to the BST's elements.</returns>
//    public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator()
//    {
//      return GetEnumerator(TraversalMethod.InOrder);
//    }
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//    {
//      throw new System.NotImplementedException();
//    }

//    /// <summary>
//    /// Enumerates the BST's contents using a specified traversal method.
//    /// </summary>
//    /// <param name="TraversalMethod">The type of traversal to perform.</param>
//    /// <returns>An enumerator that provides access to the BST's elements using a specified traversal technique.</returns>
//    public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator(TraversalMethod TraversalMethod)
//    {
//      // The traversal approaches are defined as public properties in the BST class...
//      // This method simply returns the appropriate property.
//      switch (TraversalMethod)
//      {
//        case TraversalMethod.PreOrder:
//          return Preorder.GetEnumerator();

//        case TraversalMethod.InOrder:
//          return Inorder.GetEnumerator();

//        case TraversalMethod.PostOrder:
//        default:
//          return Postorder.GetEnumerator();
//      }
//    }

//    #endregion
//    #endregion

//    #region Public Properties
//    #region Enumerable Properties
//    /// <summary>
//    /// Provides enumeration through the BST using preorder traversal.
//    /// </summary>
//    public System.Collections.Generic.IEnumerable<T> Preorder
//    {
//      get
//      {
//        // A single stack is sufficient here - it simply maintains the correct
//        // order with which to process the children.
//        System.Collections.Generic.Stack<BinaryTreeNode<T>> toVisit = new System.Collections.Generic.Stack<BinaryTreeNode<T>>(Count);
//        BinaryTreeNode<T> current = root;
//        if (current != null) toVisit.Push(current);

//        while (toVisit.Count != 0)
//        {
//          // take the top item from the stack
//          current = toVisit.Pop();

//          // add the right and left children, if not null
//          if (current.Right != null) toVisit.Push(current.Right);
//          if (current.Left != null) toVisit.Push(current.Left);

//          // return the current node
//          yield return current.Value;
//        }
//      }
//    }

//    /// <summary>
//    /// Provides enumeration through the BST using inorder traversal.
//    /// </summary>
//    public System.Collections.Generic.IEnumerable<T> Inorder
//    {
//      get
//      {
//        // A single stack is sufficient - this code was made available by Grant Richins:
//        // http://blogs.msdn.com/grantri/archive/2004/04/08/110165.aspx
//        System.Collections.Generic.Stack<BinaryTreeNode<T>> toVisit = new System.Collections.Generic.Stack<BinaryTreeNode<T>>(Count);
//        for (BinaryTreeNode<T> current = root; current != null || toVisit.Count != 0; current = current.Right)
//        {
//          // Get the left-most item in the subtree, remembering the path taken
//          while (current != null)
//          {
//            toVisit.Push(current);
//            current = current.Left;
//          }

//          current = toVisit.Pop();
//          yield return current.Value;
//        }
//      }
//    }

//    /// <summary>
//    /// Provides enumeration through the BST using postorder traversal.
//    /// </summary>
//    public System.Collections.Generic.IEnumerable<T> Postorder
//    {
//      get
//      {
//        // maintain two stacks, one of a list of nodes to visit,
//        // and one of booleans, indicating if the note has been processed
//        // or not.
//        System.Collections.Generic.Stack<BinaryTreeNode<T>> toVisit = new System.Collections.Generic.Stack<BinaryTreeNode<T>>(Count);
//        System.Collections.Generic.Stack<bool> hasBeenProcessed = new System.Collections.Generic.Stack<bool>(Count);
//        BinaryTreeNode<T> current = root;
//        if (current != null)
//        {
//          toVisit.Push(current);
//          hasBeenProcessed.Push(false);
//          current = current.Left;
//        }

//        while (toVisit.Count != 0)
//        {
//          if (current != null)
//          {
//            // add this node to the stack with a false processed value
//            toVisit.Push(current);
//            hasBeenProcessed.Push(false);
//            current = current.Left;
//          }
//          else
//          {
//            // see if the node on the stack has been processed
//            bool processed = hasBeenProcessed.Pop();
//            BinaryTreeNode<T> node = toVisit.Pop();
//            if (!processed)
//            {
//              // if it's not been processed, "recurse" down the right subtree
//              toVisit.Push(node);
//              hasBeenProcessed.Push(true);    // it's now been processed
//              current = node.Right;
//            }
//            else
//              yield return node.Value;
//          }
//        }
//      }
//    }
//    #endregion

//    /// <summary>
//    /// Returns the number of elements in the BST.
//    /// </summary>
//    public int Count
//    {
//      get
//      {
//        return count;
//      }
//    }

//    public bool IsReadOnly
//    {
//      get
//      {
//        return false;
//      }
//    }
//    #endregion
//  }
//}
