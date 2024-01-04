import React from 'react';
import { ToastContainer } from 'react-toastify';
import { useDarkMode } from 'usehooks-ts';

interface ToastsPlaceholderProps {}

const ToastsPlaceholder: React.FC<ToastsPlaceholderProps> = ({}) => {
	const isDarkTheme = useDarkMode();
	return <ToastContainer theme={isDarkTheme ? 'colored' : 'light'} position="bottom-right" />;
};

export default ToastsPlaceholder;
