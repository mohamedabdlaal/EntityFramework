// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public abstract class MappingQueryFixtureBase
    {
        protected abstract string DatabaseSchema { get;  }

        protected Model CreateModel()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            modelBuilder.Entity<MappingQueryTestBase.MappedCustomer>(e =>
                {
                    e.Key(c => c.CustomerID);
                    e.Property(c => c.CompanyName2).Metadata.Relational().ColumnName = "Broken";
                    e.Metadata.Relational().TableName = "Broken";
                    if (!string.IsNullOrEmpty(DatabaseSchema))
                    {
                        e.Metadata.Relational().Schema = "wrong";
                    }
                });

            modelBuilder.Entity<MappingQueryTestBase.MappedEmployee>(e =>
                {
                    e.Key(em => em.EmployeeID);
                    e.Property(em => em.City2).Metadata.Relational().ColumnName = "City";
                    e.Metadata.Relational().TableName = "Employees";
                    e.Metadata.Relational().Schema = DatabaseSchema;
                });

            modelBuilder.Entity<MappingQueryTestBase.MappedOrder>(e =>
                {
                    e.Key(o => o.OrderID);
                    e.Property(em => em.ShipVia2).Metadata.Relational().ColumnName = "ShipVia";
                    e.Metadata.Relational().TableName = "Orders";
                    e.Metadata.Relational().Schema = DatabaseSchema;
                });

            OnModelCreating(modelBuilder);

            return modelBuilder.Model;
        }

        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
