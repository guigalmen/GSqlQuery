﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    public class StatementsCollection
    {
        private readonly Dictionary<string, IStatements> _statements = new();

        /// <summary>
        /// Get IStatements by key
        /// </summary>
        /// <param name="Key">The key of the element to add.</param>
        /// <returns>Associated IStatements</returns>
        public IStatements this[string Key]
        {
            get
            {
                return _statements[Key];
            }
        }

        /// <summary>
        ///  Adds the specified key.
        /// </summary>
        /// <param name="Key">The key of the element to add</param>
        /// <param name="statements">Associated IStatements></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Add(string Key, IStatements statements)
        {
            if (string.IsNullOrWhiteSpace(Key))
                throw new ArgumentNullException(nameof(Key));

            if (statements == null)
                throw new ArgumentNullException(nameof(statements));

            _statements.Add(Key, statements);
        }

        internal string GetFirstStatements()
        {
            return _statements.Keys.Any() ? _statements.FirstOrDefault().Key : string.Empty;
        }

        public IEnumerable<string> GetAllKeys()
        {
            return _statements.Keys;
        }

        public void Clear()
        { 
            _statements.Clear();
        }
    }
}
