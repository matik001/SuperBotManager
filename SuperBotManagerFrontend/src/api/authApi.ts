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

export const signup = async (requestData: UserSignUpRequestDTO) => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/signup', requestData);
	return res.data;
};

export const signin = async (requestData: UserSignInRequestDTO) => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/signin', requestData);
	return res.data;
};

export const refreshToken = async (requestData: RefreshTokenRequstDTO) => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/refresh-token', requestData);
	return res.data;
};

export const logout = async () => {
	const res = await appAxios.post<UserTokensDTO>('/v1/Auth/logout', {});
	return res.data;
};
