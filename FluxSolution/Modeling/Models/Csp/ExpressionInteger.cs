﻿//namespace Flux.Csp
//{
//  public class ExpressionInteger
//    : Expression<int>
//  {
//    public static ExpressionInteger operator +(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value + r.Value,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.LowerBound + rightBounds.LowerBound,
//            leftBounds.UpperBound + rightBounds.UpperBound
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;
//          if (first.Bounds.LowerBound < enforce.LowerBound - second.Bounds.UpperBound)
//          {
//            first.Bounds.LowerBound = enforce.LowerBound - second.Bounds.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.UpperBound > enforce.UpperBound - second.Bounds.LowerBound)
//          {
//            first.Bounds.UpperBound = enforce.UpperBound - second.Bounds.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (second.Bounds.LowerBound < enforce.LowerBound - first.Bounds.UpperBound)
//          {
//            second.Bounds.LowerBound = enforce.LowerBound - first.Bounds.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (second.Bounds.UpperBound > enforce.UpperBound - first.Bounds.LowerBound)
//          {
//            second.Bounds.UpperBound = enforce.UpperBound - first.Bounds.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator -(ExpressionInteger left, ExpressionInteger right)
//    {
//      var expression = new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value - r.Value,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.LowerBound - rightBounds.UpperBound,
//            leftBounds.UpperBound - rightBounds.LowerBound
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;
//          if (first.Bounds.LowerBound < enforce.LowerBound + second.Bounds.LowerBound)
//          {
//            first.Bounds.LowerBound = enforce.LowerBound + second.Bounds.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.UpperBound > enforce.UpperBound + second.Bounds.UpperBound)
//          {
//            first.Bounds.UpperBound = enforce.UpperBound + second.Bounds.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (second.Bounds.LowerBound < first.Bounds.LowerBound - enforce.UpperBound)
//          {
//            second.Bounds.LowerBound = first.Bounds.LowerBound - enforce.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (second.Bounds.UpperBound > first.Bounds.UpperBound - enforce.LowerBound)
//          {
//            second.Bounds.UpperBound = first.Bounds.UpperBound - enforce.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };

//      expression.remove = prune =>
//      {
//        var result = DomainOperationResult.ElementNotInDomain;
//        if (expression.left.Bounds.UpperBound - expression.left.Bounds.LowerBound == 0)
//          result = ((ExpressionInteger)expression.right).remove(expression.left.Bounds.LowerBound - prune);

//        if (result == DomainOperationResult.EmptyDomain)
//          return result;

//        if (expression.right.Bounds.UpperBound - expression.right.Bounds.LowerBound == 0)
//          result = ((ExpressionInteger)expression.left).remove(expression.right.Bounds.LowerBound + prune);

//        return result;
//      };

//      return expression;
//    }

//    public static ExpressionInteger operator /(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value / r.Value,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.LowerBound / rightBounds.UpperBound,
//            leftBounds.UpperBound / rightBounds.LowerBound
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;
//          if (first.Bounds.LowerBound < second.Bounds.LowerBound * enforce.LowerBound)
//          {
//            first.Bounds.LowerBound = second.Bounds.LowerBound * enforce.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.UpperBound > second.Bounds.UpperBound * enforce.UpperBound)
//          {
//            first.Bounds.UpperBound = second.Bounds.UpperBound * enforce.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (enforce.UpperBound == 0)
//            return result;

//          if (second.Bounds.LowerBound < first.Bounds.LowerBound / enforce.UpperBound)
//          {
//            second.Bounds.LowerBound = first.Bounds.LowerBound / enforce.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (enforce.LowerBound == 0)
//            return result;

//          if (second.Bounds.UpperBound > first.Bounds.UpperBound / enforce.LowerBound)
//          {
//            second.Bounds.UpperBound = first.Bounds.UpperBound / enforce.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator *(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value * r.Value,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.LowerBound * rightBounds.LowerBound,
//            leftBounds.UpperBound * rightBounds.UpperBound
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (second.Bounds.UpperBound == 0 || second.Bounds.LowerBound == 0)
//            return result;

//          if (first.Bounds.LowerBound < enforce.LowerBound / second.Bounds.UpperBound)
//          {
//            first.Bounds.LowerBound = enforce.LowerBound / second.Bounds.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.UpperBound > enforce.UpperBound / second.Bounds.LowerBound)
//          {
//            first.Bounds.UpperBound = enforce.UpperBound / second.Bounds.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.UpperBound == 0 || first.Bounds.LowerBound == 0)
//            return result;

//          if (second.Bounds.LowerBound < enforce.LowerBound / first.Bounds.UpperBound)
//          {
//            second.Bounds.LowerBound = enforce.LowerBound / first.Bounds.UpperBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (second.Bounds.UpperBound > enforce.UpperBound / first.Bounds.LowerBound)
//          {
//            second.Bounds.UpperBound = enforce.UpperBound / first.Bounds.LowerBound;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator &(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => (l.Value != 0) && (r.Value != 0) ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            (leftBounds.LowerBound != 0) && (rightBounds.LowerBound != 0) ? 1 : 0,
//            (leftBounds.UpperBound != 0) && (rightBounds.UpperBound != 0) ? 1 : 0
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0)
//          {
//            if (first.Bounds.UpperBound == 0 || second.Bounds.UpperBound == 0)
//              result = ConstraintOperationResult.Violated;
//            else
//            {
//              first.Bounds.LowerBound = 1;
//              second.Bounds.LowerBound = 1;
//              result = ConstraintOperationResult.Propagated;
//            }
//          }
//          else if (enforce.UpperBound == 0)
//          {
//            if (first.Bounds.LowerBound == 1)
//            {
//              if (second.Bounds.LowerBound > 0)
//                result = ConstraintOperationResult.Violated;
//              else if (second.Bounds.UpperBound == 1)
//              {
//                second.Bounds.UpperBound = 0;
//                result = ConstraintOperationResult.Propagated;
//              }
//            }

//            if (second.Bounds.LowerBound == 1)
//            {
//              if (second.Bounds.LowerBound > 0)
//                result = ConstraintOperationResult.Violated;
//              else if (first.Bounds.UpperBound == 1)
//              {
//                first.Bounds.UpperBound = 0;
//                result = ConstraintOperationResult.Propagated;
//              }
//            }
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator |(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => (l.Value != 0) || (r.Value != 0) ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            (leftBounds.LowerBound != 0) || (rightBounds.LowerBound != 0) ? 1 : 0,
//            (leftBounds.UpperBound != 0) || (rightBounds.UpperBound != 0) ? 1 : 0
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0)
//          {
//            if (first.Bounds.UpperBound == 0)
//            {
//              second.Bounds.LowerBound = 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.UpperBound == 0)
//            {
//              first.Bounds.LowerBound = 1;
//              result = ConstraintOperationResult.Propagated;
//            }
//          }
//          else if (enforce.UpperBound == 0)
//          {
//            first.Bounds.UpperBound = 0;
//            second.Bounds.UpperBound = 0;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator ^(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => ((l.Value != 0) || (r.Value != 0)) && ((l.Value == 0) || (r.Value == 0)) ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            (leftBounds.LowerBound == leftBounds.UpperBound) && (rightBounds.LowerBound > 0) &&
//              (leftBounds.LowerBound != rightBounds.LowerBound) ? 1 : 0,
//            (leftBounds.LowerBound == leftBounds.UpperBound) && (rightBounds.LowerBound == rightBounds.UpperBound) &&
//              (leftBounds.LowerBound == rightBounds.LowerBound) ? 0 : 1
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0)
//          {
//            if (first.Bounds.UpperBound == 0)
//            {
//              second.Bounds.LowerBound = 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.LowerBound == 1)
//            {
//              second.Bounds.UpperBound = 0;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.UpperBound == 0)
//            {
//              first.Bounds.LowerBound = 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.LowerBound == 1)
//            {
//              first.Bounds.UpperBound = 0;
//              result = ConstraintOperationResult.Propagated;
//            }
//          }
//          else if (enforce.UpperBound == 0)
//          {
//            if (first.Bounds.UpperBound == 0)
//            {
//              second.Bounds.UpperBound = 0;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.LowerBound == 1)
//            {
//              second.Bounds.LowerBound = 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.UpperBound == 0)
//            {
//              first.Bounds.UpperBound = 0;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.LowerBound == 1)
//            {
//              first.Bounds.LowerBound = 1;
//              result = ConstraintOperationResult.Propagated;
//            }
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator !(ExpressionInteger operand)
//    {
//      return new ExpressionInteger(operand, null!)
//      {
//        evaluate = (l, r) => l.Value == 0 ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var bounds = l.GetUpdatedBounds();

//          return new Bounds<int>(bounds.UpperBound == 0 ? 1 : 0, bounds.LowerBound == 0 ? 1 : 0);
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.UpperBound == 0)
//          {
//            first.Bounds.LowerBound = 1;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (enforce.LowerBound > 0)
//          {
//            first.Bounds.UpperBound = 0;
//            result = ConstraintOperationResult.Propagated;
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator <(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value < r.Value ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.UpperBound < rightBounds.LowerBound ? 1 : 0,
//            leftBounds.LowerBound < leftBounds.UpperBound ? 1 : 0
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0) // enforce a < b
//          {
//            if (second.Bounds.LowerBound <= first.Bounds.LowerBound)
//            {
//              second.Bounds.LowerBound = first.Bounds.LowerBound + 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound >= second.Bounds.UpperBound)
//            {
//              first.Bounds.UpperBound = second.Bounds.UpperBound - 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.LowerBound >= second.Bounds.UpperBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }
//          else if (enforce.UpperBound == 0) // enforce a >= b
//          {
//            if (first.Bounds.LowerBound < second.Bounds.LowerBound)
//            {
//              first.Bounds.LowerBound = second.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.UpperBound > first.Bounds.UpperBound)
//            {
//              second.Bounds.UpperBound = first.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound < second.Bounds.LowerBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator >(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value > r.Value ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.LowerBound > rightBounds.UpperBound ? 1 : 0,
//            leftBounds.UpperBound > leftBounds.LowerBound ? 1 : 0
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0) // enforce a > b
//          {
//            if (first.Bounds.LowerBound <= second.Bounds.LowerBound)
//            {
//              first.Bounds.LowerBound = second.Bounds.LowerBound + 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.UpperBound >= first.Bounds.UpperBound)
//            {
//              second.Bounds.UpperBound = first.Bounds.UpperBound - 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound <= second.Bounds.LowerBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }
//          else if (enforce.UpperBound == 0) // enforce a <= b
//          {
//            if (second.Bounds.LowerBound < first.Bounds.LowerBound)
//            {
//              second.Bounds.LowerBound = first.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound > second.Bounds.UpperBound)
//            {
//              first.Bounds.UpperBound = second.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.LowerBound > second.Bounds.UpperBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator <=(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value <= r.Value ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.UpperBound <= rightBounds.LowerBound ? 1 : 0,
//            leftBounds.LowerBound <= leftBounds.UpperBound ? 1 : 0
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0) // enforce a <= b
//          {
//            if (second.Bounds.LowerBound < first.Bounds.LowerBound)
//            {
//              second.Bounds.LowerBound = first.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound > second.Bounds.UpperBound)
//            {
//              first.Bounds.UpperBound = second.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.LowerBound > second.Bounds.UpperBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }
//          else if (enforce.UpperBound == 0) // enforce a > b
//          {
//            if (first.Bounds.LowerBound <= second.Bounds.LowerBound)
//            {
//              first.Bounds.LowerBound = second.Bounds.LowerBound + 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.UpperBound >= first.Bounds.UpperBound)
//            {
//              second.Bounds.UpperBound = first.Bounds.UpperBound - 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound <= second.Bounds.LowerBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator >=(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value >= r.Value ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.LowerBound >= rightBounds.UpperBound ? 1 : 0,
//            leftBounds.UpperBound >= leftBounds.LowerBound ? 1 : 0
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0) // enforce a >= b
//          {
//            if (first.Bounds.LowerBound < second.Bounds.LowerBound)
//            {
//              first.Bounds.LowerBound = second.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (second.Bounds.UpperBound > first.Bounds.UpperBound)
//            {
//              second.Bounds.UpperBound = first.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound < second.Bounds.LowerBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }
//          else if (enforce.UpperBound == 0) // enforce a < b
//          {
//            if (second.Bounds.LowerBound <= first.Bounds.LowerBound)
//            {
//              second.Bounds.LowerBound = first.Bounds.LowerBound + 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound >= second.Bounds.UpperBound)
//            {
//              first.Bounds.UpperBound = second.Bounds.UpperBound - 1;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.LowerBound >= second.Bounds.UpperBound)
//            {
//              result = ConstraintOperationResult.Violated;
//            }
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator ==(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value == r.Value ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.LowerBound == leftBounds.UpperBound && rightBounds.LowerBound == rightBounds.UpperBound &&
//            leftBounds.LowerBound == rightBounds.LowerBound ? 1 : 0,
//            leftBounds.UpperBound < rightBounds.LowerBound || leftBounds.LowerBound > rightBounds.UpperBound ? 0 : 1
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.LowerBound > 0)
//          {
//            if (first.Bounds.LowerBound < second.Bounds.LowerBound)
//            {
//              first.Bounds.LowerBound = second.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }
//            else if (second.Bounds.LowerBound < first.Bounds.LowerBound)
//            {
//              second.Bounds.LowerBound = first.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound < second.Bounds.UpperBound)
//            {
//              second.Bounds.UpperBound = first.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }
//            else if (second.Bounds.UpperBound < first.Bounds.UpperBound)
//            {
//              first.Bounds.UpperBound = second.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public static ExpressionInteger operator !=(ExpressionInteger left, ExpressionInteger right)
//    {
//      return new ExpressionInteger(left, right)
//      {
//        evaluate = (l, r) => l.Value != r.Value ? 1 : 0,
//        evaluateBounds = (l, r) =>
//        {
//          var leftBounds = l.GetUpdatedBounds();
//          var rightBounds = r.GetUpdatedBounds();

//          return new Bounds<int>
//          (
//            leftBounds.UpperBound < rightBounds.LowerBound || leftBounds.LowerBound > rightBounds.UpperBound ? 1 : 0,
//            leftBounds.LowerBound == leftBounds.UpperBound && rightBounds.LowerBound == rightBounds.UpperBound &&
//            leftBounds.LowerBound == rightBounds.LowerBound ? 0 : 1
//          );
//        },
//        propagator = (first, second, enforce) =>
//        {
//          var result = ConstraintOperationResult.Undecided;

//          if (enforce.LowerBound == 0 && enforce.LowerBound < enforce.UpperBound)
//            return result;

//          if (enforce.UpperBound == 0)
//          {
//            if (first.Bounds.LowerBound < second.Bounds.LowerBound)
//            {
//              first.Bounds.LowerBound = second.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }
//            else if (second.Bounds.LowerBound < first.Bounds.LowerBound)
//            {
//              second.Bounds.LowerBound = first.Bounds.LowerBound;
//              result = ConstraintOperationResult.Propagated;
//            }

//            if (first.Bounds.UpperBound < second.Bounds.UpperBound)
//            {
//              second.Bounds.UpperBound = first.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }
//            else if (second.Bounds.UpperBound < first.Bounds.UpperBound)
//            {
//              first.Bounds.UpperBound = second.Bounds.UpperBound;
//              result = ConstraintOperationResult.Propagated;
//            }
//          }
//          else
//          {
//            if (first.Bounds.UpperBound == first.Bounds.LowerBound &&
//              second.remove(first.Bounds.LowerBound) == DomainOperationResult.EmptyDomain)
//              return ConstraintOperationResult.Violated;

//            if (second.Bounds.UpperBound == second.Bounds.LowerBound &&
//              first.remove(second.Bounds.LowerBound) == DomainOperationResult.EmptyDomain)
//              return ConstraintOperationResult.Violated;
//          }

//          if (first.Bounds.LowerBound > first.Bounds.UpperBound || second.Bounds.LowerBound > second.Bounds.UpperBound)
//            result = ConstraintOperationResult.Violated;

//          return result;
//        }
//      };
//    }

//    public override bool Equals(object? obj)
//      => base.Equals(obj);
//    public override int GetHashCode()
//      => base.GetHashCode();

//    protected Expression<int> left;
//    protected Expression<int> right;
//    protected int integer;
//    protected System.Func<ExpressionInteger, ExpressionInteger, int> evaluate;
//    protected System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>> evaluateBounds;
//    protected System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>, ConstraintOperationResult> propagator;
//    protected System.Func<int, DomainOperationResult> remove;

//    [System.CLSCompliant(false)]
//    public Expression<int> Left { get { return this.left; } }
//    [System.CLSCompliant(false)]
//    public Expression<int> Right { get { return this.right; } }
//    [System.CLSCompliant(false)]
//    public int Integer { get { return this.integer; } }
//    [System.CLSCompliant(false)]
//    public System.Func<ExpressionInteger, ExpressionInteger, int> Evaluate { get { return this.evaluate; } }
//    [System.CLSCompliant(false)]
//    public System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>> EvaluateBounds { get { return this.evaluateBounds; } }
//    [System.CLSCompliant(false)]
//    public System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>, ConstraintOperationResult> Propagator { get { return this.propagator; } }

//    public override bool IsBound
//    {
//      get
//      {
//        if (((object)this.left) == null && ((object)this.right) == null)
//          return true;

//        return ((object)this.right) == null ? this.left.IsBound : this.left.IsBound && this.right.IsBound;
//      }
//    }

//    public override Bounds<int> GetUpdatedBounds()
//    {
//      if (this.Evaluate == null)
//      {
//        if (this.left is VariableInteger)
//          this.Bounds = this.left.GetUpdatedBounds();

//        return this.Bounds;
//      }

//      this.Bounds = this.EvaluateBounds((ExpressionInteger)this.left, (ExpressionInteger)this.right);
//      return this.Bounds;
//    }

//    public override int Value
//    {
//      get
//      {
//        if (this.Evaluate == null)
//          return this.left is VariableInteger ? this.left.Value : this.integer;

//        return this.Evaluate((ExpressionInteger)this.left, (ExpressionInteger)this.right);
//      }
//    }

//    public override void Propagate(Bounds<int> enforceBounds, out ConstraintOperationResult result)
//    {
//      left.GetUpdatedBounds();

//      if (right != null)
//        right.GetUpdatedBounds();

//      var propagated = false;
//      var intermediateResult = propagator((ExpressionInteger)left, (ExpressionInteger)right, enforceBounds);

//      while (intermediateResult == ConstraintOperationResult.Propagated)
//      {
//        var leftResult = ConstraintOperationResult.Undecided;
//        var rightResult = ConstraintOperationResult.Undecided;

//        if (!left.IsBound)
//          left.Propagate(left.Bounds, out leftResult);

//        if (right != null && !right.IsBound)
//          right.Propagate(right.Bounds, out rightResult);

//        intermediateResult = (leftResult | rightResult) & ConstraintOperationResult.Propagated;
//        if (intermediateResult != ConstraintOperationResult.Propagated)
//          continue;

//        propagated = true;
//        intermediateResult = propagator((ExpressionInteger)left, (ExpressionInteger)right, enforceBounds);
//      }

//      if (intermediateResult == ConstraintOperationResult.Violated)
//        result = ConstraintOperationResult.Violated;
//      else
//        result = propagated ? ConstraintOperationResult.Propagated : ConstraintOperationResult.Undecided;
//    }

//    public ExpressionInteger(Expression<int> left, Expression<int> right)
//    {
//      this.left = left;
//      this.right = right;
//    }

//    public ExpressionInteger(int integer)
//    {
//      this.integer = integer;
//      Bounds = new Bounds<int>(integer, integer);
//      remove = _ => DomainOperationResult.ElementNotInDomain;
//    }

//    internal ExpressionInteger(VariableInteger variable,
//      System.Func<ExpressionInteger, ExpressionInteger, int> evaluate,
//      System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>> evaluateBounds,
//      System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>, ConstraintOperationResult> propagator)
//    {
//      this.left = variable;
//      this.evaluate = evaluate;
//      this.evaluateBounds = evaluateBounds;
//      this.propagator = propagator;
//    }

//    public static implicit operator ExpressionInteger(int i)
//    {
//      return new ExpressionInteger(i);
//    }

//    internal ExpressionInteger() { }
//  }
//}
