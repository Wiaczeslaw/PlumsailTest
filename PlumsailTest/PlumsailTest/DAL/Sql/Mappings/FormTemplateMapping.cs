using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlumsailTest.Domain.Entities;

namespace PlumsailTest.DAL.Sql.Mappings
{
    public class FormTemplateMapping : IEntityTypeConfiguration<FormTemplate>
    {
        public void Configure(EntityTypeBuilder<FormTemplate> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            
            builder.HasMany(x => x.ItemTemplates)
                .WithOne(x => x.FormTemplate)
                .HasForeignKey(x => x.FormTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("FormTemplates");
        }
    }
}