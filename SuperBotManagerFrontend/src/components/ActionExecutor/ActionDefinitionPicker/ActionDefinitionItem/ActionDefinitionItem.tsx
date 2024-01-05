import { Tag, Tooltip } from 'antd';
import { ActionDefinitionDTO } from 'api/actionDefinitionApi';
import React from 'react';
import { styled } from 'styled-components';

interface ActionDefinitionItemProps {
	actionDefinition: ActionDefinitionDTO;
	onClick: () => void;
}

const ItemContainer = styled.div`
	display: flex;
	gap: 10px;
	align-items: center;
	padding: 8px;
	cursor: pointer;
	user-select: none;
	transition: all 0.3s;
	border-radius: 8px;
	background-color: ${(a) => a.theme.secondaryColor};
	&:hover {
		background-color: ${(a) => a.theme.bgColor2};
	}
	&:active {
		background-color: ${(a) => a.theme.bgColor3};
	}
	margin: 5px 0;
`;

const ActionDefinitionItem: React.FC<ActionDefinitionItemProps> = ({
	actionDefinition,
	onClick
}) => {
	return (
		<ItemContainer onClick={onClick}>
			<img
				src={actionDefinition.actionDefinitionIcon}
				style={{
					width: '100px',
					height: '100px',
					borderRadius: '8px'
				}}
			/>
			<div>
				<div style={{ fontSize: '22px' }}>{actionDefinition.actionDefinitionName}</div>
				<div style={{ fontSize: '18px', fontWeight: '100' }}>
					{actionDefinition.actionDefinitionDescription}
				</div>
				<div style={{ margin: '2px 0' }}>
					<span style={{ marginRight: '10px' }}>Input fields</span>
					{actionDefinition.actionDataSchema.inputSchema.map((field) => (
						<Tooltip color="orange" key={field.name} title={field.description}>
							<Tag color="geekblue-inverse">
								{field.name} ({field.type})
							</Tag>
						</Tooltip>
					))}
				</div>
				<div>
					<span style={{ marginRight: '10px' }}>Output fields</span>
					{actionDefinition.actionDataSchema.outputSchema.map((field) => (
						<Tooltip color="orange" key={field.name} title={field.description}>
							<Tag color="green-inverse">
								{field.name} ({field.type})
							</Tag>
						</Tooltip>
					))}
				</div>
			</div>
		</ItemContainer>
	);
};

export default ActionDefinitionItem;
