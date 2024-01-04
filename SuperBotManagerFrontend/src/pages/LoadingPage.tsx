import { Spin } from 'antd';
import styled from 'styled-components';

const Container = styled.div`
	width: 100%;
	height: 100%;
	display: flex;
	justify-content: center;
	align-items: center;
	user-select: none;
`;
const LoadingPage = () => {
	return (
		<Container>
			<Spin fullscreen />
		</Container>
	);
};

export default LoadingPage;
