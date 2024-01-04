import NotFoundPage from 'pages/NotFoundPage';
import SignInPage from 'pages/SignInPage';
import SignUpPage from 'pages/SignUpPage';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import useAuthStore from 'store/authStore';

const Router = () => {
	const tokens = useAuthStore((a) => a.tokens);
	const isLoggedIn = !!tokens;
	return (
		<BrowserRouter>
			<Routes>
				{isLoggedIn ? (
					<>
						<Route path="/signin" element={<Navigate to="/" />} />
						<Route path="/signup" element={<Navigate to="/" />} />
					</>
				) : (
					<>
						<Route path="/signin" element={<SignInPage />} />
						<Route path="/signup" element={<SignUpPage />} />
					</>
				)}
				<Route path="/" element={<Navigate to="/signin" />} />

				<Route path="*" element={<NotFoundPage />} />
			</Routes>
		</BrowserRouter>
	);
};

export default Router;
