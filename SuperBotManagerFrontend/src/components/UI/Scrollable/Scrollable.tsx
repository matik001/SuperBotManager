import { css } from 'styled-components';

export const ScrollableMixin = css`
	overflow-y: auto;
	height: 100%;
	max-height: 100%;
	&::-webkit-scrollbar {
		width: 10px;
		height: 11px;
	}
	&::-webkit-scrollbar-track {
		box-shadow: nset 0 0 6px gray;
	}
	&::-webkit-scrollbar-thumb {
		background-color: #adadae;
		border-radius: 5px;
		border: 2px solid white;
	}
`;
