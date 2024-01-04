import axios from 'axios';
import i18next from 'i18next';
import { toast } from 'react-toastify';
import useAuthStore from 'store/authStore';

export const appAxios = axios.create({
	baseURL: '/api'
});

appAxios.interceptors.request.use(
	function (config) {
		config.headers['Authorization'] = 'Bearer ' + useAuthStore.getState().tokens?.token;

		return config;
	},
	function (error) {
		if (!axios.isCancel(error)) {
			toast.error(`Something went wrong: ${(error as Error).message}`); /// TODO dodac tlumaczenie
			console.error(error);
		}
		return Promise.reject(error);
	}
);

appAxios.interceptors.response.use(
	function (response) {
		return response;
	},
	function (error) {
		if (!axios.isCancel(error)) {
			if (axios.isAxiosError(error)) {
				const backendMessage = error.response?.data?.Message;
				if (backendMessage) {
					const errorMessage = i18next.t('Server error') + ':\n' + backendMessage;
					toast.error(errorMessage);
					console.error(error);
					return Promise.reject(error);
				}
			}
			toast.error(`Something went wrong:\n${(error as Error).message}`); /// TODO dodac tlumaczenie
			console.error(error);
		}

		return Promise.reject(error);
	}
);

export interface BackendError {
	StatusCode: number;
	Message: string;
	StackTrace?: string;
}
