using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.DAL.Sql.Mappings
{
    public class FormMapping : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            
            builder.HasOne(x => x.FormTemplate)
                .WithMany()
                .HasForeignKey(x => x.FormTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(x => x.FormItems)
                .WithOne(x => x.Form)
                .HasForeignKey(x => x.FormId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("FormTemplates");
        }
    }
}