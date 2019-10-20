using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {

	public sealed class PermissionRank : IComparable<PermissionRank> {

		private static readonly Dictionary<string, PermissionRank> ranksByName = new Dictionary<string, PermissionRank>();
		private static readonly List<PermissionRank> ranksByOrdinal = new List<PermissionRank>();

		public static readonly PermissionRank USER = new PermissionRank("user", "000000");//Regular user (default)
		public static readonly PermissionRank MODERATOR = new PermissionRank("moderator", "CC30A0");//Moderator of a group
		public static readonly PermissionRank OFFICER = new PermissionRank("officer", "00DDDD");//Officer of a group (Improved Moderator)
		public static readonly PermissionRank ADMINISTRATOR = new PermissionRank("administrator", "FFAA00");//Administrator of a group.
		public static readonly PermissionRank OWNER = new PermissionRank("owner", "00CC00");//Owner of a group
		public static readonly PermissionRank SUPERUSER = new PermissionRank("superuser", "FF0000");//Superuser

		private PermissionRank(string name, string color) {
			Ordinal = ranksByOrdinal.Count;
			Name = name;
			Color = color;

			AddPermissionRank(this);
		}

		public int Ordinal { get; private set; }
		public string Name { get; private set; }
		public string Color { get; private set; }

		public override int GetHashCode() {
			return Ordinal * 31;
		}

		public override bool Equals(object obj) {
			if (obj is PermissionRank rank) {
				if (Ordinal == rank.Ordinal) {
					return true;
				}
			}

			return false;
		}

		public override string ToString() {
			return Name;
		}

		public int CompareTo(PermissionRank other) {
			if (other is null) {
				throw new ArgumentNullException(nameof(other));
			}

			return Ordinal - other.Ordinal;
		}

		public static PermissionRank GetPermissionRankByOrdinal(int o) {
			return ranksByOrdinal[o];
		}

		public static PermissionRank GetPermissionRankByName(string name) {
			return ranksByName[name];
		}

		private static void AddPermissionRank(PermissionRank rank) {
			ranksByName.Add(rank.Name, rank);
			ranksByOrdinal.Add(rank);
		}

		public static bool operator ==(PermissionRank left, PermissionRank right) {
			if (left is null) {
				return right is null;
			}

			return left.Equals(right);
		}

		public static bool operator !=(PermissionRank left, PermissionRank right) {
			return !(left == right);
		}

		public static bool operator <(PermissionRank left, PermissionRank right) {
			return left is null ? right is object : left.CompareTo(right) < 0;
		}

		public static bool operator <=(PermissionRank left, PermissionRank right) {
			return left is null || left.CompareTo(right) <= 0;
		}

		public static bool operator >(PermissionRank left, PermissionRank right) {
			return left is object && left.CompareTo(right) > 0;
		}

		public static bool operator >=(PermissionRank left, PermissionRank right) {
			return left is null ? right is null : left.CompareTo(right) >= 0;
		}
	}

}
