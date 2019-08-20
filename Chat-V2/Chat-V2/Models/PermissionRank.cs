using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {

	public sealed class PermissionRank : IComparable<PermissionRank> {

		private static readonly SortedList<string, PermissionRank> ranks = new SortedList<string, PermissionRank>();

		public static readonly PermissionRank USER = new PermissionRank("user", "000000");//Regular user (default)
		public static readonly PermissionRank MODERATOR = new PermissionRank("moderator", "EECCCC");//Moderator of a group
		public static readonly PermissionRank OFFICER = new PermissionRank("officer", "CCCCEE");//Officer of a group (Improved Moderator)
		public static readonly PermissionRank ADMINISTRATOR = new PermissionRank("administrator", "EEDDBB");//Administrator of a group.
		public static readonly PermissionRank OWNER = new PermissionRank("owner", "00FF00");//Owner of a group
		public static readonly PermissionRank SUPERUSER = new PermissionRank("superuser", "FF0000");//Administrator of Global chat group

		private PermissionRank(string name, string color) {
			Ordinal = ranks.Values.Count + 1;
			Name = name;
			Color = color;

			ranks.Add(Name, this);
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
			return Ordinal - other.Ordinal;
		}

		public static PermissionRank GetPermissionRankByOrdinal(int o) {
			return ranks.Values[o];
		}

		public static PermissionRank GetPermissionRankByName(string name) {
			return ranks[name];
		}
	}

}
