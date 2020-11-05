namespace TradingBlockApiTestHarness.DTO.Enums
{
    public enum enumAccountType
    {
        /// <summary>
        /// An individual account is an account that is owned by one person.
        /// </summary>
        Individual = 1,
        /// <summary>
        /// Joint Tenants with Rights of Survivorship accounts are accounts where all the assets
        /// are transferred into the name of the surviving party in the event of one tenant's
        /// death. The surviving party becomes the sole owner of the assets in the account. Both
        /// parties on the account have an equal and undivided interest in the assets in the
        /// account.
        /// </summary>
        JTWROS = 2,
        /// <summary>
        /// Traditional individual retirement accounts allow individuals to direct pretax income
        /// towards investments that can grow tax-deferred; no capital gains or dividend income
        /// is taxed until it is withdrawn.
        /// </summary>
        Traditional_IRA = 3,
        /// <summary>
        /// An account that is opened by an individual and managed by a designated trustee for
        /// the benefit of a third-party in accordance with agreed-upon terms.
        /// </summary>
        Trust = 4,
        /// <summary>
        /// Individual retirement arrangement rollover accounts are a transfer of funds from a
        /// retirement account into a traditional IRA or a Roth IRA.
        /// </summary>
        IRA_RollOver = 5,
        /// <summary>
        /// Roth individual retirement accounts are a type of qualified retirement plan that
        /// is very similar to a traditional IRA, but is taxed differently. Roth IRAs are funded
        /// with after-tax dollars so contributions are not tax deductible, but when you start
        /// withdrawing funds, qualified distributions are tax free.
        /// </summary>
        Roth_IRA = 6,
        /// <summary>
        /// Simplified employee pension plan IRAs are retirement savings plans established by
        /// employers, including self-employed individuals, for the benefit of their employees.
        /// Employers may make tax-deductible contributions on behalf of eligible employees,
        /// and are made on a discrtionary basis. If the employer does contribute, it must
        /// contribute the same percentage of compensation to all employees eligible for the
        /// plan.
        /// </summary>
        SEP_IRA = 7,
        /// <summary>
        /// Joint Tenants in Common accounts are owned by at least two people with no rights of
        /// survivorship afforded to any of the account holders. In these accounts, a surviving
        /// tenant of the account does not necessarily acquire the rights and assets of the
        /// deceased person. Instead, each tenant can stipulate in a written will how his/her
        /// assets will be distributed upon his/her death.
        /// </summary>
        JTIC = 8,
        /// <summary>
        /// The corporate resolution will specify which officers may trade in the corporation's
        /// account. In addition, a certified copy of the corporate charter and the company's
        /// bylaws are required to open a margin account. There are two sub-categories:
        /// S-Corp and C-Corp.
        /// </summary>
        Corporate = 9,
        /// <summary>
        /// Limited Liability Company is a corporate structure whereby the members of the
        /// company cannot be held personally liable for the company's debts or liabilities.
        /// LLCs are essentially hybrid entities that combine the characteristics of a
        /// corporation and a partnership or sole proprietorship.
        /// </summary>
        LLC = 10,
        /// <summary>
        /// Partnerships are an arrangement by which two or more persons agree to share in all
        /// assets, profits and financial and legal liabilities of a business. Such partnerships
        /// have unlimited liability, which means their personal assets are liable to the
        /// partnership's obligations.
        /// </summary>
        Partnership = 11,
        /// <summary>
        /// Qualified Plans meet Internal Revenue Code Section 401(1) and are therefore eligible
        /// to receive certain tax benefits. They are established by an employer for the benefit
        /// of the company's employees, and give employers a tax break for the contributions
        /// they make for their employees. They may also allow employees to defer a portion of
        /// their salaries into the plan to reduce employees' present income-tax liability by
        /// reducing taxable income.
        /// </summary>
        Qualified_Plan = 12,
        /// <summary>
        /// If the taxpayer is a sole business owner or professional in their own practice, they
        /// are usually practicing as a sole proprietor. In this type of entity, you report
        /// business profit or loss on your personal tax return.
        /// </summary>
        Sole_Proprietorship = 13,
        /// <summary>
        /// An Investment Club is a group of people who pool their money to make investments.
        /// Usually, investment clubs are organized as partnerships and after the members study
        /// different investments, the group decides to buy or sell based on a majority vote of
        /// the members.
        /// </summary>
        InvestmentClub = 14,
        /// <summary>
        /// Tenants by Entirety accounts are joint accounts in which married couples can hold
        /// the title to a property. In order for one spouse to modify his or her interest in
        /// the property in any way, the consent of both spouses is required by tenants by
        /// entirety. It also provides that when one spouse passes away, the surviving spouse
        /// gains full ownership of the property.
        /// </summary>
        TBE = 15,
        /// <summary>
        /// Joint Community Property (also known as marital property) accounts refer to a U.S.
        /// state-level legal distinction of a married individual's assets. Property acquired
        /// by either spouse during a marriage is considered community property, belonging to
        /// both partners of marriage.
        /// </summary>
        CommunityProperty = 16,
        /// <summary>
        /// Coverdell / Educational IRAs allow individuals to contribute up to $2,000 in
        /// after-tax dollars to an educational IRA for each student under the age of 18
        /// years of age. The money is allowed to grow tax deferred and the growth may be
        /// withdrawn tax free, as long as the money is used for educational purposes.
        /// </summary>
        Coverdell_IRA = 17,
        /// <summary>
        /// Uniform Gifts to Minors Act (UGMA) and Uniform Trasfers to Minors Act (UTMA)
        /// allow minors to own assets including securities. Individuals can establish UGMA
        /// accounts on behalf of minors or beneficiaries, eliminating the need for an
        /// attorney to establish a special trust fund.
        /// </summary>
        UGMA = 18
    }
}
