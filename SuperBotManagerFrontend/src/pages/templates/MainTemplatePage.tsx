import LogoImageSvg from 'assets/logo.svg?raw';
import TopMenu from 'components/TopMenu/TopMenu';
import { CSSProperties, ReactNode } from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import styled, { useTheme } from 'styled-components';

const Title = styled.h1`
	text-align: center;
	font-weight: 400;
	font-size: 28px;
	pointer-events: none;
	user-select: none;
	display: flex;
	flex-direction: row;
	align-items: center;
	justify-content: center;
	color: ${(a) => a.theme.textColor};
	text-decoration: none !important;
`;

interface MainTemplatePageProps {
	children: ReactNode;
	style?: CSSProperties;
}
const Layout = styled.div`
	height: 100vh;
	width: 100%;
	display: flex;
	flex-direction: column;
	position: relative;
	overflow: hidden;
`;
const MenuRow = styled.div`
	display: flex;
	flex-direction: row;
	align-items: center;
	padding: 16px;
	background-color: ${(props) => props.theme.secondaryBgColor};
	width: 100%;
`;
const MainTemplatePage = ({ children, style }: MainTemplatePageProps) => {
	const { t } = useTranslation();
	const theme = useTheme();
	return (
		<Layout>
			<MenuRow>
				<Link to="/" style={{ textDecoration: 'none' }}>
					<Title>
						<div
							style={{
								marginRight: '3px',
								color: theme.primaryColor
							}}
							dangerouslySetInnerHTML={{ __html: LogoImageSvg }}
						></div>

						{t('App title')}
					</Title>
				</Link>
				<TopMenu />
			</MenuRow>

			<div
				style={{
					flex: 1,
					position: 'relative',
					width: '100%',
					flexGrow: '1',
					minHeight: 0,
					...style
				}}
			>
				{children}
			</div>
		</Layout>
	);
};

export default MainTemplatePage;
