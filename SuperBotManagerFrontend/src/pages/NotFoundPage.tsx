import { Button } from 'antd';
import notFoundImg from 'assets/NotFound.jpg';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { MdHome } from 'react-icons/md';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import MainTemplatePage from './templates/MainTemplatePage';

interface NotFoundPageProps {}

const Container = styled.div`
	width: 100%;
	height: 100vh;
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;
`;
const Text = styled.div`
	font-size: 30px;
	user-select: none;
`;

const Image = styled.img`
	object-fit: fill;
	width: 100%;
	height: 100%;
	box-sizing: border-box;
	padding: 5px;
`;
const ImageEdgeBlur = styled.div`
	margin-top: -50px;
	flex: 0;
	min-width: 0;
	width: 40%;
	position: relative;
	text-align: center;

	&::after {
		box-shadow: inset 0px 0px 15px 30px ${(p) => p.theme.bgColor};
		content: '';
		display: block;
		height: 100%;
		position: absolute;
		top: 0;
		width: 100%;
	}
`;
const NotFoundPage: React.FC<NotFoundPageProps> = ({}) => {
	const { t } = useTranslation();

	return (
		<MainTemplatePage>
			<Container>
				<ImageEdgeBlur>
					<Image src={notFoundImg}></Image>
				</ImageEdgeBlur>
				<Text>404 - {t('Not found')}</Text>
				<Link
					to="/"
					style={{
						textDecorationLine: 'none'
					}}
				>
					<Button
						style={{
							fontSize: '26px',
							height: '54px',
							display: 'flex',
							alignItems: 'center',
							gap: '3px',
							marginTop: '30px'
						}}
					>
						<MdHome />
						{t('Back home')}
					</Button>
				</Link>
			</Container>
		</MainTemplatePage>
	);
};

export default NotFoundPage;
