import { Spin } from 'antd';
import React, { ReactNode, useEffect } from 'react';
import useAuthStore from 'store/authStore';
import useUserStore from 'store/userStore';
interface AppAuthProviderProps {
	children: ReactNode;
}
const AppAuthProvider: React.FC<AppAuthProviderProps> = ({ children }) => {
	const wasHydrated = useAuthStore((a) => a.wasHydrated);
	const user = useAuthStore((a) => a.tokens?.user);
	const setUser = useUserStore((a) => a.setUser);

	/// saving user info from one store to another
	useEffect(() => {
		setUser(user);
	}, [setUser, user]);

	/// it is waiting until useAuthStore reads localstorage tokens, saves it in memory and sends refresh-token request
	return wasHydrated ? children : <Spin fullscreen></Spin>;
};

export default AppAuthProvider;
