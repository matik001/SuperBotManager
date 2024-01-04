import i18n from 'i18next';
import common_en from 'locale/en/common.json';
import common_pl from 'locale/pl/common.json';
import LoadingPage from 'pages/LoadingPage';
import React, { ReactNode, useEffect, useState } from 'react';
import { initReactI18next } from 'react-i18next';
interface AppTranslationsProviderProps {
	children: ReactNode;
}

i18n
	.use(initReactI18next)
	// .use(LanguageDetector)
	.init({
		resources: {
			en: {
				common: common_en
			},
			pl: {
				common: common_pl
			}
		},
		fallbackLng: 'en',
		defaultNS: 'common',
		fallbackNS: 'common',
		interpolation: {
			escapeValue: false
		}
	});

const AppTranslationsProvider: React.FC<AppTranslationsProviderProps> = ({ children }) => {
	const [isLoading, setIsLoading] = useState(true);
	useEffect(() => {
		setIsLoading(false);
	}, []);

	/// todo: dodac pobieranie tlumaczen z backendu tylko potrzebnych, zamiast tak jak teraz wszystkich
	/// gdy projekt sie bardzo rozwinie i będzie wiecej plikow niz tylko common.json bedzie to mialo sens

	return <>{isLoading ? <LoadingPage /> : children}</>;
};

export default AppTranslationsProvider;
