using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {

	public sealed class PermissionRank : IComparable<PermissionRank> {

		private static readonly SortedList<string, PermissionRank> ranks = new SortedList<string, PermissionRank>();

		public static readonly PermissionRank USER = new PermissionRank("user");//Regular user (default)
		public static readonly PermissionRank MODERATOR = new PermissionRank("moderator");//Moderator of a group
		public static readonly PermissionRank OFFICER = new PermissionRank("officer");//Officer of a group (Improved Moderator)
		public static readonly PermissionRank ADMINISTRATOR = new PermissionRank("administrator");//Administrator of a group.
		public static readonly PermissionRank OWNER = new PermissionRank("owner");//Owner of a group
		public static readonly PermissionRank SUPERUSER = new PermissionRank("superuser");//Administrator of Global chat group

		private PermissionRank(string name) {
			Ordinal = ranks.Values.Count + 1;
			Name = name;

			ranks.Add(Name, this);
		}

		public int Ordinal { get; set; }
		public string Name { get; set; }

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
