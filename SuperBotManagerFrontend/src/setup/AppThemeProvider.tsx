import { ConfigProvider, theme as antdTheme } from 'antd';
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
h1{
	color: ${(args) => args.theme.primaryColor};

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
    box-shadow: inset 0 0 20px 20px ${(p) => p.theme.secondaryColor};
}

`;
export interface AppTheme {
	primaryColor: string;
	secondaryColor: string;
	bgColor: string;
	textColor: string;
	dangerColor: string;
	isDarkMode: boolean;
}
declare module 'styled-components' {
	// eslint-disable-next-line @typescript-eslint/no-empty-interface
	export interface DefaultTheme extends AppTheme {}
}
const AppThemeProvider: React.FC<AppThemeProviderProps> = ({ children }) => {
	const { isDarkMode } = useDarkMode(true);

	const themeDark: AppTheme = {
		primaryColor: '#c8923b',
		secondaryColor: '#262421',
		textColor: 'white',
		bgColor: '#161513',
		dangerColor: '#e13f3a',
		isDarkMode: true
	};
	const themeLight: AppTheme = {
		primaryColor: '#c8923b',
		secondaryColor: 'white',
		bgColor: '#ecebe9',
		textColor: 'black',
		dangerColor: '#e13f3a',
		isDarkMode: false
	};
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
									colorBgBase: theme.bgColor
								},
								algorithm: darkAlgorithm
							}
						: {
								token: {
									colorPrimary: theme.primaryColor
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
