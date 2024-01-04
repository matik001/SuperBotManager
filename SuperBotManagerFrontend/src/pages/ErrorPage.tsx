import { Alert } from 'antd';
import React from 'react';
import styled from 'styled-components';

interface ErrorPageProps {
	error: Error;
}
const Container = styled.div`
	margin: 0 20%;
`;
const Header = styled.h1`
	margin: 30px auto;
	text-align: center;
`;

const ErrorPage: React.FC<ErrorPageProps> = ({ error }) => {
	return (
		<Container>
			<Header>Something went wrong :(</Header>
			<Alert message={error.message} description={error.stack} type="error" showIcon />
		</Container>
	);
};

export default ErrorPage;
