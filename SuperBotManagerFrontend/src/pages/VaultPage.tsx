import Vault from 'components/Vaults/Vaults';
import { Navigate } from 'react-router-dom';
import useAuthStore from 'store/authStore';
import MainTemplatePage from './templates/MainTemplatePage';

const VaultPage = () => {
	const currentUser = useAuthStore((a) => a.tokens?.user);
	if (currentUser === undefined) {
		return <Navigate to="/" />;
	}
	return (
		<MainTemplatePage>
			<Vault user={currentUser} />
		</MainTemplatePage>
	);
};

export default VaultPage;
