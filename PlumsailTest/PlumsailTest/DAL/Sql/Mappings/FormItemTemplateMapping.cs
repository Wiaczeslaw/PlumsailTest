using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlumsailTest.Domain.Entities;

namespace PlumsailTest.DAL.Sql.Mappings
{
    public class FormItemTemplateMapping : IEntityTypeConfiguration<FormItemTemplate>
    {
        public void Configure(EntityTypeBuilder<FormItemTemplate> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Order).IsRequired(false);
            builder.Property(x => x.Type).IsRequired();

            builder.HasOne(x => x.FormTemplate)
                .WithMany(x => x.ItemTemplates)
                .HasForeignKey(x => x.FormTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(x => x.Values)
                .WithOne(x => x.FormItemTemplate)
                .HasForeignKey(x => x.FormItemTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("FormItemTemplates");
        }
    }
}