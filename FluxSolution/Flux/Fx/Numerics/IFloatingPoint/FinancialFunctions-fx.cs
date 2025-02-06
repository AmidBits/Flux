namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The interest on loans and mortgages that are amortized, i.e. have a smooth monthly payment until the loan has been paid off, is often compounded monthly.</para>
    /// <para>The fixed monthly payment for a fixed rate mortgage is the amount paid by the borrower every month that ensures that the loan is paid off in full with interest at the end of its term. The monthly payment formula is based on the annuity formula.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Compound_interest#Monthly_amortized_loan_or_mortgage_payments"/></para>
    /// <example>For example, for a home loan of $200,000 with a fixed yearly interest rate of 6.5% for 30 years, the principal is <param name="p"/> = 200,000, the monthly interest rate is <paramref name="r"/> = 0.065 / 12, the number of monthly payments is <paramref name="n"/> = 30 * 12 = 360, the fixed monthly payment equals $1,264.14: <code>AmortizedMonthlyPayment(200000, 0.065 / 12, 30 * 12);</code></example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="p">The principal. The amount borrowed, known as the loan's principal.</param>
    /// <param name="r">The monthly interest rate. Since the quoted yearly percentage rate is not a compounded rate, the monthly percentage rate is simply the yearly percentage rate divided by 12.</param>
    /// <param name="n">The number of payment periods. (E.g. the number of monthly payments, called the loan's term.)</param>
    /// <returns>The monthly payment (c).</returns>
    public static TNumber AmortizedMonthlyPayment<TNumber>(this TNumber p, TNumber r, TNumber n)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      => r * p / (TNumber.One - (TNumber.One / TNumber.Pow(TNumber.One + r, n)));

    /// <summary>
    /// <para>Compound interest is interest accumulated from a principal sum and previously accumulated interest. It is the result of reinvesting or retaining interest that would otherwise be paid out, or of the accumulation of debts from a borrower.</para>
    /// <para>The accumulation function shows what $1 grows to after any length of time.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Compound_interest#Accumulation_function"/></para>
    /// </summary>
    /// <remarks>
    /// <para>The total accumulated value, including the principal sum P plus compounded interest I is given by: <code>A = P * CompoundInterest(r, n, t);</code></para>
    /// <para>The total compound interest generated is the final value (A) minus the initial principal: <code>I = P * CompoundInterest(r, n, t) - P;</code> or <code>I = A - P;</code></para>
    /// </remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="r">The nominal annual interest rate.</param>
    /// <param name="n">The compounding frequency.</param>
    /// <param name="t">The overall length of time the interest is applied (expressed using the same time units as r, usually years).</param>
    /// <returns>The accumulative compound interest of <paramref name="r"/>.</returns>
    public static TNumber CompoundInterest<TNumber>(this TNumber r, TNumber n, TNumber t)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      => TNumber.Pow(TNumber.One + (r / n), t * n);
  }
}
