using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.SharedModels.Common.IAM.Authorization
{
    /// <summary>
    /// Jogosultsági "zászlók" felsorolása.
    /// </summary>
    public enum PermissionFlags
    {
        /// <summary>
        /// Bejelentkezési jogosultság.
        /// </summary>
        CAN_LOGIN,
        /// <summary>
        /// Jogosultságok hozzárendelésének jogosultsága.
        /// </summary>
        CAN_ASSIGN_PERMISSIONS,
        /// <summary>
        /// Felhasználók listázásának jogosultsága.
        /// </summary>
        CAN_MODIFY_OTHER_USER,
        /// <summary>
        /// Felhasználók törlésének jogosultsága.
        /// </summary>
        CAN_DELETE_USERS,
        CAN_ADD_NEW_USERS,
        CAN_VIEW_ANIMAL_TYPES,
        CAN_VIEW_OTHERS_ANIMALS,
        CAN_VIEW_OTHER_USER,
        CAN_MODIFY_OTHER_USER_PASSWORD,
        CAN_MODIFY_ANIMAL_BREED,
        CAN_DELETE_BREED,
        CAN_ADD_NEW_BREED,
    }
}
