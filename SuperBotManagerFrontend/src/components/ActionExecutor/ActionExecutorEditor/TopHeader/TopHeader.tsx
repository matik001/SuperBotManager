import { Button, Popconfirm, Tooltip } from 'antd';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { IoMdTrash } from 'react-icons/io';
import { IoArrowBack } from 'react-icons/io5';
import { styled } from 'styled-components';

interface TopHeaderProps {
	onGoBack: () => void;
	hasUnsaveChanges: boolean;
	title: string;
	canSave: boolean;
	isDeleting: boolean;
	onDelete: () => void;
	onSave: () => void;
	iconUrl?: string;
}

const Container = styled.div`
	background-color: ${(t) => (t.theme.isDarkMode ? t.theme.secondaryBgColor : t.theme.bgColor2)};
	padding: 5px 20px 5px 10px;
	display: flex;
	flex-flow: row nowrap;
	align-items: center;
	justify-content: space-between;
`;

const Row = styled.div`
	display: flex;
	flex-direction: row;
	align-items: center;
	gap: 6px;
`;
const TopHeader: React.FC<TopHeaderProps> = ({
	onGoBack,
	hasUnsaveChanges,
	iconUrl,
	title,
	canSave,
	isDeleting,
	onSave,
	onDelete
}) => {
	const [isUnsavedMsgPopupOpen, setUnsavedMsgPopupOpen] = useState(false);
	const { t } = useTranslation();
	return (
		<Container>
			<Popconfirm
				title={t('Do you want to leave?')}
				description={t('Unsaved changes will be lost.')}
				okText={t('Yes')}
				cancelText={t('No')}
				onConfirm={onGoBack}
				onCancel={() => {}}
				open={isUnsavedMsgPopupOpen}
				onOpenChange={(open) => {
					if (open && !hasUnsaveChanges) onGoBack();
					setUnsavedMsgPopupOpen(open);
				}}
				// onPopupClick={(e) => (!hasUnsaveChanges && onGoBack()) || setUnsavedMsgPopupOpen(true)}
			>
				<Button
					icon={<IoArrowBack />}
					shape="circle"
					type="text"
					style={{ width: '42px', height: '42px', fontSize: '22px' }}
					onClick={(e) => e.stopPropagation()}
				/>
			</Popconfirm>

			<Row>
				{iconUrl && (
					<img style={{ width: '28px', height: '28px', borderRadius: '6px' }} src={iconUrl}></img>
				)}
				{title}
			</Row>

			<Row>
				<Button disabled={!canSave} type="primary" onClick={() => onSave()}>
					{t('Save')}
				</Button>
				<Popconfirm
					title={t('Are you sure?')}
					description={t('You will delete also connected actions!')}
					placement="bottomLeft"
					okText="Yes"
					cancelText="No"
					onConfirm={onDelete}
					onCancel={() => {}}
					// onPopupClick={(e) => (!hasUnsaveChanges && onGoBack()) || setUnsavedMsgPopupOpen(true)}
				>
					<Tooltip title={t('Delete')}>
						<Button loading={isDeleting} danger type="default" icon={<IoMdTrash />}></Button>
					</Tooltip>
				</Popconfirm>
			</Row>
		</Container>
	);
};

export default TopHeader;
