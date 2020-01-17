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
        public int LineNumber { get; set; }

        public string ConcatAB { get => SumCD.HasValue && SumCD.Value > 100 ? ColumnA + ColumnB : null; }

        public int? SumCD { get => ColumnC.HasValue && ColumnD.HasValue ? ColumnC.Value + ColumnD.Value : (int?)null; }

        public string Message
        {
            get
            {
                if (!ColumnC.HasValue)
                    return "Column C is missing or has bad format";
                if (!ColumnD.HasValue)
                    return "Column D is missing or has bad format";
                if (ConcatAB == null)
                    return "SumCD is lower or equal to 100";
                return null;
            }
        }

        public bool IsOk => Message == null;

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
                && string.Equals(OtherColumn, other.OtherColumn)
                && LineNumber == LineNumber;
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
                hashCode = (hashCode * 397) ^ LineNumber;
                return hashCode;
            }
        }

        public override string ToString()
            => $"{Column ?? ""},{ColumnA ?? ""}, {ColumnB ?? ""}, {(ColumnC.HasValue ? ColumnC.Value.ToString() : "")}, {(ColumnD.HasValue ? ColumnD.Value.ToString() : "")}, {OtherColumn ?? ""}";
    }

}
