import { UserDTO } from 'api/userApi';
import { v4 as uuidv4 } from 'uuid';
import { create } from 'zustand';
import { devtools } from 'zustand/middleware';
import { immer } from 'zustand/middleware/immer';

export interface UserStoreState {
	nick: string; /// if signin it is username
	guestGuid?: string;
	user?: UserDTO; /// only if signed in
}
export interface UserStoreActions {
	setNickname: (nick: string) => void;
	setUser: (user: UserDTO | undefined) => void;
}

const genOrRestoreGuestGuid = () => {
	const guestGuid = localStorage.getItem('guestGuid');
	if (guestGuid) return guestGuid;
	const newGuid = uuidv4();
	localStorage.setItem('guestGuid', newGuid);
	return newGuid;
};
export const restoreUserNick = () => {
	const nick = localStorage.getItem('nick');
	if (nick) return nick;
	return 'Guest';
};
const saveUserNick = (nick: string) => {
	localStorage.setItem('nick', nick);
};
const StoreName = 'UserStore';
const useUserStore = create<UserStoreState & UserStoreActions>()(
	immer(
		devtools(
			(set, get) =>
				({
					nick: restoreUserNick(),
					user: undefined,
					/// TODO: for security concerns it should be generated and validated in backend, now anyone can pretend to be another guest user
					guestGuid: genOrRestoreGuestGuid(),

					setUser: (user: UserDTO | undefined) => {
						if (get().user === user) return;
						const nick = user?.userName ?? 'Guest';
						set({
							user: user,
							nick: nick,
							guestGuid: user?.userName != null ? undefined : genOrRestoreGuestGuid()
						});
						saveUserNick(nick);
					},
					setNickname: (nick) => {
						set({
							nick: nick
						});
						saveUserNick(nick);
					}
				}) as UserStoreState & UserStoreActions,
			{ name: StoreName, store: StoreName }
		)
	)
);
export default useUserStore;
