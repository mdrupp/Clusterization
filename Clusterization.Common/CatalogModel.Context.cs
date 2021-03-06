﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CatalogData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BiblioEntities : DbContext
    {
        public BiblioEntities()
            : base("name=BiblioEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CentroidCluster> CentroidClusters { get; set; }
        public DbSet<ClusteredBook> ClusteredBooks { get; set; }
        public DbSet<ClusterError> ClusterErrors { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<LastBookUpdate> LastBookUpdates { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }
        public DbSet<Snippet> Snippets { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordsFrequency> WordsFrequencies { get; set; }
    }
}
