module.exports = {
  root: true,
  env: { browser: true, es2020: true },
  extends: [
    'eslint:recommended',
		'plugin:react/recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:react-hooks/recommended',
    'eslint-config-prettier'
  ],
  ignorePatterns: ['dist', '.eslintrc.cjs'],
  parser: '@typescript-eslint/parser',
  plugins: ['react-refresh', 'react', '@typescript-eslint', 'prettier', 'eslint-plugin-prettier'],
  rules: {
    'react-refresh/only-export-components': [
      'warn',
      { allowConstantExport: true },
    ],
		'react-refresh/only-export-components': 'warn',
		'react/react-in-jsx-scope': 'off',
		'@typescript-eslint/no-empty-interface': 'warn',
		'@typescript-eslint/no-unused-vars': 'warn',
		'prettier/prettier': 'warn',
		'@typescript-eslint/no-empty-function': 'warn',
		'no-empty-pattern': 'warn',
		'no-mixed-spaces-and-tabs': 'warn',
		'react-hooks/rules-of-hooks': 'error',
		'react-hooks/exhaustive-deps': 'error',
    '@typescript-eslint/no-explicit-any': 'warn'
  },
}
