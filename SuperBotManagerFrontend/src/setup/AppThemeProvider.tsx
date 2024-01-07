import { ConfigProvider, theme as antdTheme } from 'antd';
import { darken } from 'polished';
import React, { ReactNode } from 'react';
import { ThemeProvider, createGlobalStyle } from 'styled-components';
import { useDarkMode } from 'usehooks-ts';
interface AppThemeProviderProps {
	children: ReactNode;
}
const GlobalStyle = createGlobalStyle`
body, html, #root {
  width: 100%;
  min-height: 100vh;
  margin: 0;
  background-color: ${(args) => args.theme.bgColor};
  color: ${(args) => args.theme.textColor};
  position: relative;
  overflow-x: hidden;

}

*{
	margin: 0;
	padding: 0;
	box-sizing: border-box;
}

/* https://stackoverflow.com/questions/2781549/removing-input-background-colour-for-chrome-autocomplete */
input:-webkit-autofill,
input:-webkit-autofill:hover, 
input:-webkit-autofill:focus, 
input:-webkit-autofill:active{
    -webkit-background-clip: text;
	-webkit-text-fill-color: ${(p) => p.theme.textColor} !important;
    transition: background-color 5000s ease-in-out 0s;
    box-shadow: inset 0 0 20px 20px ${(p) => p.theme.secondaryBgColor};
}
*{
	/* font-weight: 100; */
}
`;
export interface AppTheme {
	primaryColor: string;
	bgColor: string;
	secondaryBgColor: string;
	textColor: string;

	successColor: string;
	warningColor: string;
	errorColor: string;

	bgColor2: string;
	bgColor3: string;

	isDarkMode: boolean;
}
declare module 'styled-components' {
	// eslint-disable-next-line @typescript-eslint/no-empty-interface
	export interface DefaultTheme extends AppTheme {}
}

export const themeDark: AppTheme = {
	primaryColor: '#FA8616',
	// secondaryBgColor: 'rgba(255, 255, 255, 0.12)',
	secondaryBgColor: '#262421',

	bgColor2: darken(0.05, '#262421'),
	bgColor3: darken(0.08, '#262421'),
	textColor: 'white',
	bgColor: '#161513',
	errorColor: '#FF4D4F',
	successColor: '#52c41a',
	warningColor: '#FAAD14',
	isDarkMode: true
};
export const themeLight: AppTheme = {
	primaryColor: '#FA8616',
	secondaryBgColor: 'white',
	bgColor2: darken(0.05, '#ecebe9'),
	bgColor3: darken(0.08, '#ecebe9'),
	bgColor: '#ecebe9',
	textColor: 'black',
	errorColor: '#FF4D4F',
	successColor: '#52c41a',
	warningColor: '#FAAD14',
	isDarkMode: false
};

const AppThemeProvider: React.FC<AppThemeProviderProps> = ({ children }) => {
	const { isDarkMode } = useDarkMode(true);

	const theme = isDarkMode ? themeDark : themeLight;
	const { defaultAlgorithm, darkAlgorithm } = antdTheme;

	return (
		<ThemeProvider theme={theme}>
			<ConfigProvider
				theme={
					isDarkMode
						? {
								token: {
									colorPrimary: theme.primaryColor,
									colorText: theme.textColor,
									colorBgBase: theme.bgColor,

									colorError: theme.errorColor,
									colorWarning: theme.warningColor,
									colorSuccess: theme.successColor
								},
								algorithm: darkAlgorithm
							}
						: {
								token: {
									colorPrimary: theme.primaryColor,
									colorText: theme.textColor
								},
								algorithm: defaultAlgorithm
							}
				}
			>
				<>
					<GlobalStyle />
					{children}
				</>
			</ConfigProvider>
		</ThemeProvider>
	);
};

export default AppThemeProvider;
