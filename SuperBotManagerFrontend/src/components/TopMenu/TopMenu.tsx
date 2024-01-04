import { useMutation } from '@tanstack/react-query';
import { Button, Switch } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { MdOutlineDarkMode, MdOutlineWbSunny } from 'react-icons/md';
import { useLocation, useNavigate } from 'react-router-dom';
import useAuthStore from 'store/authStore';
import useUserStore from 'store/userStore';
import { styled } from 'styled-components';
import { useDarkMode } from 'usehooks-ts';

const ThemeSwitch = styled(Switch)`
	& .ant-switch-handle::before {
		background-color: black;
	}
	margin-left: 7px;
`;
const DarkModeIcon = styled(MdOutlineDarkMode)`
	font-size: 18px;
`;
const LightModeIcon = styled(MdOutlineWbSunny)`
	font-size: 18px;
`;

interface TopMenuProps {}

const TopMenu: React.FC<TopMenuProps> = ({}) => {
	const isAuthenticated = !!useAuthStore((a) => a.tokens);
	const logout = useAuthStore((a) => a.logout);

	const navigate = useNavigate();
	const logoutMutation = useMutation({
		mutationFn: async () => {
			await logout();
		},
		onSuccess: () => {
			navigate('/signin');
		}
	});
	const location = useLocation();
	const { isDarkMode, toggle } = useDarkMode(true);
	const { t, i18n } = useTranslation();
	const nickname = useUserStore((a) => a.nick);

	return (
		<>
			{/* <Button
				style={{ marginLeft: '10px', height: '40px' }}
				type={location.pathname == '/something' ? 'primary' : 'text'}
				onClick={() => navigate('/something')}
			>
				{t('something')}
			</Button> */}
			{isAuthenticated ? (
				<>
					<Button
						loading={logoutMutation.isPending}
						style={{ marginLeft: '10px', height: '40px' }}
						type="text"
						onClick={() => logoutMutation.mutate()}
					>
						{t('Logout')}
					</Button>
				</>
			) : (
				<>
					<Button
						style={{ marginLeft: '10px', height: '40px' }}
						type={location.pathname == '/signin' ? 'primary' : 'text'}
						onClick={() => navigate('/signin')}
					>
						{t('Sign in')}
					</Button>

					<Button
						style={{ marginLeft: '10px', height: '40px' }}
						type={location.pathname == '/signup' ? 'primary' : 'text'}
						onClick={() => navigate('/signup')}
					>
						{t('Sign up')}
					</Button>
				</>
			)}

			<Button
				style={{ marginLeft: 'auto' }}
				type="primary"
				ghost
				// ghost={location.pathname == '/profile' ? false : true}
				onClick={() => navigate('/profile')}
			>
				{nickname}
			</Button>
			<Button
				type="text"
				onClick={() => {
					i18n.changeLanguage(i18n.language === 'pl' ? 'en' : 'pl');
				}}
			>
				{i18n.languages[0] === 'pl' ? 'PL' : 'ENG'}
			</Button>
			<ThemeSwitch
				checked={isDarkMode}
				onClick={toggle}
				checkedChildren={<DarkModeIcon />}
				unCheckedChildren={<LightModeIcon />}
			/>
		</>
	);
};

export default TopMenu;
