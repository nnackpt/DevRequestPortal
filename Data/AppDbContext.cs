using DevRequestPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace DevRequestPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Proposal> Proposals => Set<Proposal>();
        public DbSet<ProposalAttachment> ProposalAttachments => Set<ProposalAttachment>();
        public DbSet<Annotation> Annotations => Set<Annotation>();
        public DbSet<AnnotationAttachment> AnnotationAttachments => Set<AnnotationAttachment>();
    }
}