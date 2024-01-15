import ActionExecutorEditPage from 'pages/ActionExecutorEditPage';
import ActionExecutorsPage from 'pages/ActionExecutorsPage';
import NotFoundPage from 'pages/NotFoundPage';
import SchedulePage from 'pages/SchedulePage';
import SignInPage from 'pages/SignInPage';
import SignUpPage from 'pages/SignUpPage';
import VaultPage from 'pages/VaultPage';
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
						<Route path="/executors" element={<ActionExecutorsPage />} />
						<Route path="/executor/edit/:id" element={<ActionExecutorEditPage />} />
						<Route path="/schedule" element={<SchedulePage />} />
						<Route path="/vault" element={<VaultPage />} />
						<Route path="/" element={<Navigate to="/executors" />} />
					</>
				) : (
					<>
						<Route path="/signin" element={<SignInPage />} />
						<Route path="/signup" element={<SignUpPage />} />
						<Route path="*" element={<Navigate to="/signin" />} />
					</>
				)}

				<Route path="*" element={<NotFoundPage />} />
			</Routes>
		</BrowserRouter>
	);
};

export default Router;
