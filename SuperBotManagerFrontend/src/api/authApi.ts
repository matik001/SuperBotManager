import { appAxios } from './apiConfig';
import { UserDTO } from './userApi';

export interface UserSignUpRequestDTO {
	userName: string;
	email: string;
	password: string;
}
export interface UserSignInRequestDTO {
	userName: string;
	password: string;
}
export interface UserTokensDTO {
	token: string;
	refreshToken: string;
	tokenExpiration: Date;
	refreshTokenExpiration: Date;
	user: UserDTO;
}
export interface RefreshTokenRequstDTO {
	token: string;
	refreshToken: string;
}

export const QUERYKEY_SIGNUP = 'QUERYKEY_SIGNUP';
export const signup = async (requestData: UserSignUpRequestDTO) => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/signup', requestData);
	return res.data;
};

export const QUERYKEY_SIGNIN = 'QUERYKEY_SIGNIN';
export const signin = async (requestData: UserSignInRequestDTO) => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/signin', requestData);
	return res.data;
};

export const QUERYKEY_REFRESHTOKEN = 'QUERYKEY_REFRESHTOKEN';
export const refreshToken = async (requestData: RefreshTokenRequstDTO) => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/refresh-token', requestData);
	return res.data;
};

export const QUERYKEY_LOGOUT = 'QUERYKEY_LOGOUT';
export const logout = async () => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/logout', {});
	return res.data;
};
