import { useQuery } from '@tanstack/react-query';
import { Input, Modal } from 'antd';
import { ColumnsType } from 'antd/es/table';
import {
	ActionDefinitionDTO,
	FieldInfo,
	QUERYKEY_ACTIONDEFINITION_GETALL,
	actionDefinitionGetAll
} from 'api/actionDefinitionApi';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import React, { useEffect, useMemo, useState } from 'react';
import styled, { useTheme } from 'styled-components';
import ActionDefinitionItem from './ActionDefinitionItem/ActionDefinitionItem';

interface ActionDefinitionPickerProps {
	isOpen: boolean;
	onPick: (actionDefinition: ActionDefinitionDTO) => void;
	onClose: () => void;
}

const fieldColumns: ColumnsType<FieldInfo> = [
	{
		title: 'Field name',
		dataIndex: 'name',
		key: 'name'
	},
	{
		title: 'Description',
		dataIndex: 'description',
		key: 'description'
	},
	{
		title: 'Type',
		dataIndex: 'type',
		key: 'type'
	}
];
const Container = styled.div`
	${ScrollableMixin}
	height: calc(100% - 50px);
`;
const ActionDefinitionPicker: React.FC<ActionDefinitionPickerProps> = ({
	isOpen,
	onPick,
	onClose
}) => {
	const { data: actionDefinitions, isFetching } = useQuery({
		queryKey: [QUERYKEY_ACTIONDEFINITION_GETALL],
		queryFn: ({ signal }) => actionDefinitionGetAll(signal)
	});
	const theme = useTheme();
	const [searchPhrase, setSearchPhrase] = useState('');

	const filteredItems = useMemo(() => {
		return actionDefinitions?.filter(
			(a) =>
				a.actionDefinitionName.toLowerCase().includes(searchPhrase.toLowerCase()) ||
				a.actionDefinitionDescription.toLowerCase().includes(searchPhrase.toLowerCase())
		);
	}, [actionDefinitions, searchPhrase]);
	useEffect(() => {
		if (isOpen) setSearchPhrase('');
	}, [isOpen]);
	return (
		<Modal
			style={{ userSelect: 'none' }}
			styles={{
				content: { height: '90vh', overflow: 'hidden' },
				body: { overflow: 'hidden', height: '100%' }
			}}
			closeIcon={false}
			width={'80%'}
			footer={null}
			centered
			open={isOpen}
			title={
				<div
					style={{
						display: 'flex',
						flexDirection: 'row',
						alignItems: 'center',
						marginBottom: '20px'
					}}
				>
					<div style={{ flexGrow: 1, fontSize: '30px' }}>Pick an action to create</div>
					Search
					<Input
						value={searchPhrase}
						placeholder="Enter a phrase to search"
						style={{ width: '250px', fontSize: '18px', marginLeft: '10px' }}
						onChange={(text) => setSearchPhrase(text.target.value)}
					/>
				</div>
			}
			onCancel={onClose}
		>
			<Container>
				{isFetching && <Spinner />}

				{filteredItems?.map((action) => (
					<ActionDefinitionItem
						key={action.id}
						actionDefinition={action}
						onClick={() => onPick(action)}
					/>
				))}
				{filteredItems?.length === 0 && (
					<div
						style={{
							position: 'absolute',
							display: 'flex',
							alignItems: 'center',
							justifyContent: 'center',
							width: '100%',
							height: 'calc(100% - 140px)',
							fontSize: '28px',
							fontWeight: '100'
						}}
					>
						Not found
					</div>
				)}
			</Container>
		</Modal>
	);
};

export default ActionDefinitionPicker;
