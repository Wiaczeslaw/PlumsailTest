using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.DAL.Sql.Mappings
{
    public class FormItemSelectValueMapping : IEntityTypeConfiguration<FormItemSelectValue>
    {
        public void Configure(EntityTypeBuilder<FormItemSelectValue> builder)
        {
            builder.Property(x => x.Value).IsRequired();

            builder.HasOne(x => x.FormItemTemplate)
                .WithMany(x => x.Values)
                .HasForeignKey(x => x.FormItemTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("FormItemSelectValues");
        }
    }
}