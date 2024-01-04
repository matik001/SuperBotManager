export interface RoleDTO {
	id: number;
	roleName: string;
}
export interface UserDTO {
	id: number;
	userName: string;
	userEmail: string;
	createdDate: Date;
	modifiedDate: Date;
}

// export const QUERYKEY_GETUSER = 'QUERYKEY_GETCURRENTUSER';
// export const getUser = async (userId: number) => {
// 	const res = await appAxios.get<UserDTO>('/v1/User/current');
// 	return res.data;
// };
