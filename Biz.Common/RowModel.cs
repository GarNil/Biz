using System;
using System.Diagnostics.CodeAnalysis;

namespace Biz.Common
{
    public class RowModel : IEquatable<RowModel>
    {
        public string Column { get; set; }
        public string ColumnA { get; set; }
        public string ColumnB { get; set; }
        public int? ColumnC { get; set; }
        public int? ColumnD { get; set; }
        public string OtherColumn { get; set; }

        public bool Equals([AllowNull] RowModel other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return true
                && string.Equals(Column, other.Column)
                && string.Equals(ColumnA, other.ColumnA)
                && string.Equals(ColumnB, other.ColumnB)
                && ColumnC == other.ColumnC
                && ColumnD == other.ColumnD
                && string.Equals(OtherColumn, other.OtherColumn);
        }

        public override bool Equals(object obj)
            => Equals(obj as RowModel);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (Column?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ColumnA?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ColumnB?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ColumnC?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ColumnD?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (OtherColumn?.GetHashCode() ?? 0);
                return hashCode;

            }
        }

        public override string ToString()
            => $"{Column ?? ""},{ColumnA ?? ""}, {ColumnB ?? ""}, {(ColumnC.HasValue ? ColumnC.Value.ToString() : "")}, {(ColumnD.HasValue ? ColumnD.Value.ToString() : "")}, {OtherColumn ?? ""}";
    }

}
