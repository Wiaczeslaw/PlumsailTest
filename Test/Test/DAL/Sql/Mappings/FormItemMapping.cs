using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.DAL.Sql.Mappings
{
    public class FormItemMapping : IEntityTypeConfiguration<FormItem>
    {
        public void Configure(EntityTypeBuilder<FormItem> builder)
        {
            builder.Property(x => x.Value).IsRequired(false);

            builder.HasOne(x => x.FormItemTemplate)
                .WithMany()
                .HasForeignKey(x => x.FormItemTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Form)
                .WithMany(x => x.FormItems)
                .HasForeignKey(x => x.FormId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.FormItemSelectValue)
                .WithMany()
                .HasForeignKey(x => x.FormItemSelectValueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("FormItems");
        }
    }
}