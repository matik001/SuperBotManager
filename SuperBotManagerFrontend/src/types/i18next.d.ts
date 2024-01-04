import 'i18next';
import common_en from 'locale/en/common.json';
declare module 'i18next' {
	interface CustomTypeOptions {
		defaultNS: 'common';
		resources: {
			common: typeof common_en;
		};
	}
}
