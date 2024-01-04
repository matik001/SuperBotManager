import { Alert } from 'antd';
import axios from 'axios';
import React from 'react';
import styled from 'styled-components';
import { BackendError } from '../../api/apiConfig';

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

const GlobalErrorInfo: React.FC<ErrorPageProps> = ({ error }) => {
	let message = error.message;
	let stack = error.stack;
	if (axios.isAxiosError(error)) {
		if (error.response?.status === 500 && error.response.data) {
			const backendError = error.response.data as BackendError;
			message = `${backendError.StatusCode} - ${backendError.Message}`;
			stack = backendError.StackTrace;
		}
	}
	return (
		<Container>
			<Header>Something went wrong :(</Header>
			<Alert message={message} description={stack} type="error" showIcon />
		</Container>
	);
};

export default GlobalErrorInfo;
