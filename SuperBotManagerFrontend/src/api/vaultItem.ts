import { appAxios } from './apiConfig';
export type RunMethod = 'Manual' | 'Automatic';

export const vaultItemKeys = {
	prefix: ['vaultItem'] as const,
	list: () => [...vaultItemKeys.prefix, 'list'] as const,
	one: (id: number) => [...vaultItemKeys.prefix, 'one', id] as const
};

export interface VaultItemUpdateDTO {
	id: number;
	vaultGroupName: string;
	fieldName: string;
	ownerId: number;
	secretId?: string;
	plainValue: string | null; /// if set, secret will be changed (backend will not provide it)
}

export type VaultItemDTO = Omit<VaultItemUpdateDTO, 'plainValue'> & {
	createdDate: Date;
	modifiedDate: Date;
};

export const vaultItemGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<VaultItemDTO[]>('/v1/VaultItem', {
		signal: signal
	});
	return res.data;
};

export const vaultItemGetOne = async (id: number, signal: AbortSignal) => {
	const res = await appAxios.get<VaultItemDTO>(`/v1/VaultItem/${id}`, {
		signal: signal
	});
	return res.data;
};

export const vaultItemPut = async (vaultItem: VaultItemUpdateDTO, signal?: AbortSignal) => {
	const res = await appAxios.put(`/v1/VaultItem/${vaultItem.id}`, vaultItem, {
		signal: signal
	});
	return res.data;
};
