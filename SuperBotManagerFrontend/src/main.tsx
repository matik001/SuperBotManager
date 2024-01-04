import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import React, { Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import { ErrorBoundary } from 'react-error-boundary';
import 'react-toastify/dist/ReactToastify.css';
import AppAuthProvider from 'setup/AppAuthProvider';
import ToastsPlaceholder from 'setup/ToastsPlaceholder';
import GlobalErrorInfo from './components/Error/GlobalErrorInfo';
import './index.css';
import LoadingPage from './pages/LoadingPage';
import AppThemeProvider from './setup/AppThemeProvider';
import AppTranslationsProvider from './setup/AppTranslationsProvider';
import Router from './setup/router/router';

const init = async () => {
	const queryClient = new QueryClient({
		defaultOptions: {
			queries: {
				refetchOnReconnect: false,
				refetchOnWindowFocus: false,
				refetchInterval: false,
				refetchOnMount: false,
				retry: 0
			}
		}
	});
	ReactDOM.createRoot(document.getElementById('root')!).render(
		<React.StrictMode>
			<Suspense fallback={<LoadingPage />}>
				<QueryClientProvider client={queryClient}>
					<AppThemeProvider>
						<AppTranslationsProvider>
							<ErrorBoundary FallbackComponent={GlobalErrorInfo}>
								<AppAuthProvider>
									<Router />
									<ReactQueryDevtools initialIsOpen={false} />
								</AppAuthProvider>
							</ErrorBoundary>
							<ToastsPlaceholder />
						</AppTranslationsProvider>
					</AppThemeProvider>
				</QueryClientProvider>
			</Suspense>
		</React.StrictMode>
	);
};

init();
