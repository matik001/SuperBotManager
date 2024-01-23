import { useQuery } from '@tanstack/react-query';
import { Input, Modal } from 'antd';
import {
	ActionDefinitionDTO,
	actionDefinitionGetAll,
	definitionKeys
} from 'api/actionDefinitionApi';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import React, { useEffect, useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { MdSearch } from 'react-icons/md';
import styled, { useTheme } from 'styled-components';
import ActionDefinitionItem from './ActionDefinitionItem/ActionDefinitionItem';

interface ActionDefinitionPickerProps {
	isOpen: boolean;
	onPick: (actionDefinition: ActionDefinitionDTO) => void;
	onClose: () => void;
}

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
		queryKey: definitionKeys.list(),
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
	const { t } = useTranslation();
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
					<div style={{ flexGrow: 1, fontSize: '30px' }}>{t('Pick an action to create')}</div>
					<Input
						prefix={<MdSearch />}
						value={searchPhrase}
						placeholder={t('Search')}
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
