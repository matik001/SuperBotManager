import React, { useRef } from 'react';
import { useTranslation } from 'react-i18next';
import { AiOutlinePlus } from 'react-icons/ai';
import { styled } from 'styled-components';
import { useHover } from 'usehooks-ts';

interface ActionExecutorItemNewProps {
	onClick: () => void;
}
const Container = styled.div`
	position: relative;
	user-select: none;
	cursor: pointer;
	width: 200px;
	height: 280px;
	background-color: ${(p) => p.theme.secondaryColor};
	opacity: 0.4;
	display: flex;
	flex-direction: column;
	justify-content: center;
	align-items: center;
	transition: all 0.3s;
	&:hover {
		opacity: 1;
	}
`;

const ActionExecutorItemNew: React.FC<ActionExecutorItemNewProps> = ({ onClick }) => {
	const { t } = useTranslation();
	const containerRef = useRef<any>();
	const hovered = useHover(containerRef);
	return (
		<Container ref={containerRef} onClick={() => onClick()}>
			<span style={{ fontSize: '50px' }}>
				<AiOutlinePlus />
			</span>
			{t('Create')}
		</Container>
	);
};

export default ActionExecutorItemNew;
