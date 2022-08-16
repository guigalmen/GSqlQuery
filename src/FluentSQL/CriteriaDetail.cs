﻿using FluentSQL.SearchCriteria;

namespace FluentSQL
{
    /// <summary>
    /// Contains the details of the criteria
    /// </summary>
    public class CriteriaDetail
    {
        /// <summary>
        /// Get Query part 
        /// </summary>
        public string QueryPart { get; }

        /// <summary>
        /// Get Parameter Details
        /// </summary>
        public IEnumerable<ParameterDetail> ParameterDetails { get; }

        /// <summary>
        /// Get Search Criteria
        /// </summary>
        public ISearchCriteria SearchCriteria { get; }

        /// <summary>
        /// Initializes a new instance of the CriteriaDetail class.
        /// </summary>
        /// <param name="searchCriteria">Search Criteria</param>
        /// <param name="queryPart">Query part</param>
        /// <param name="parameterDetails">Parameter Details</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CriteriaDetail(ISearchCriteria searchCriteria, string queryPart, IEnumerable<ParameterDetail> parameterDetails)
        {
            SearchCriteria = searchCriteria ?? throw new ArgumentNullException(nameof(searchCriteria));
            QueryPart = queryPart ?? throw new ArgumentNullException(nameof(queryPart));
            ParameterDetails = parameterDetails;
        }
    }
}
