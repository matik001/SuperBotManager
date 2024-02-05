import { ConfigProvider } from 'antd';
import enEN from 'antd/locale/en_US';
import plPL from 'antd/locale/pl_PL';
import dayjs from 'dayjs';
import duration from 'dayjs/plugin/duration';
import localizedFormat from 'dayjs/plugin/localizedFormat';
import relativeTime from 'dayjs/plugin/relativeTime';
import i18n from 'i18next';
import common_en from 'locale/en/common.json';
import common_pl from 'locale/pl/common.json';
import LoadingPage from 'pages/LoadingPage';
import React, { ReactNode, useEffect, useState } from 'react';
import { initReactI18next, useTranslation } from 'react-i18next';
import utc from 'dayjs/plugin/utc';

import('dayjs/locale/en');
import('dayjs/locale/pl');
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
	const { t, i18n } = useTranslation();
	dayjs.locale(i18n.language); // must be here, to update before rerender when changing lang
	useEffect(() => {
		dayjs.extend(relativeTime);
		dayjs.extend(localizedFormat);
		dayjs.extend(duration);
		dayjs.extend(utc);
	}, []);

	/// todo: dodac pobieranie tlumaczen z backendu tylko potrzebnych, zamiast tak jak teraz wszystkich
	/// gdy projekt sie bardzo rozwinie i będzie wiecej plikow niz tylko common.json bedzie to mialo sens

	return (
		<>
			{isLoading ? (
				<LoadingPage />
			) : (
				<ConfigProvider locale={i18n.language === 'pl' ? plPL : enEN}>{children}</ConfigProvider>
			)}
		</>
	);
};

export default AppTranslationsProvider;
