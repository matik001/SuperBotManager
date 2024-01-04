import {
	UserSignInRequestDTO,
	UserSignUpRequestDTO,
	UserTokensDTO,
	refreshToken,
	signin,
	signup
} from 'api/authApi';
import { create } from 'zustand';
import { devtools, persist } from 'zustand/middleware';
import { immer } from 'zustand/middleware/immer';
export interface AuthState {
	tokens?: UserTokensDTO;
	isLoading: boolean;
	error?: Error;

	wasHydrated: boolean;
}
export interface AuthActions {
	refreshToken: () => Promise<boolean>;
	signup: (signupData: UserSignUpRequestDTO) => Promise<UserTokensDTO>;
	signin: (signinData: UserSignInRequestDTO) => Promise<UserTokensDTO>;
	logout: () => Promise<void>;
}

const StoreName = 'AuthStore';
const useAuthStore = create<AuthState & AuthActions>()(
	persist(
		immer(
			devtools(
				(set, get) =>
					({
						tokens: undefined,
						isLoading: false,
						wasHydrated: false,

						refreshToken: async () => {
							const tokens = get().tokens;
							if (!tokens) return;
							if (tokens.refreshTokenExpiration < new Date()) {
								set({
									isLoading: false,
									tokens: undefined,
									error: undefined
								});
								return;
							}
							// set({
							// 	isLoading: true
							// });
							try {
								const newTokens = await refreshToken({
									refreshToken: tokens.refreshToken,
									token: tokens.token
								});
								set({
									isLoading: false,
									tokens: newTokens,
									error: undefined
								});
							} catch (error) {
								set({
									isLoading: false,
									tokens: undefined,
									error: error as Error
								});
							}

							return true;
						},
						logout: async () => {
							set({
								isLoading: false,
								tokens: undefined,
								error: undefined
							});
						},
						signin: async (signinData) => {
							let newTokens: UserTokensDTO | undefined = undefined;
							try {
								set({
									isLoading: true,
									error: undefined
								});
								newTokens = await signin(signinData);
								set({
									isLoading: false,
									tokens: newTokens,
									error: undefined
								});
							} catch (error) {
								set({
									isLoading: false,
									tokens: undefined,
									error: error as Error
								});
							}

							return newTokens;
						},
						signup: async (signupData) => {
							let newTokens: UserTokensDTO | undefined = undefined;
							try {
								set({
									isLoading: true,
									error: undefined
								});
								newTokens = await signup(signupData);
								set({
									isLoading: false,
									tokens: newTokens,
									error: undefined
								});
							} catch (error) {
								set({
									isLoading: false,
									tokens: undefined,
									error: error as Error
								});
							}

							return newTokens;
						}
					}) as AuthState & AuthActions,
				{ name: StoreName, store: StoreName }
			)
		),
		{
			name: StoreName,
			onRehydrateStorage: (state) => async (state) => {
				setTimeout(async () => {
					await state!.refreshToken();
					useAuthStore.setState({
						wasHydrated: true
					});
					setInterval(() => useAuthStore.getState().refreshToken(), 300000); /// refresh token every 5 min
				}, 0);
			},
			partialize: (state) => ({
				tokens: state.tokens
			})
		}
	)
);
export default useAuthStore;
