import { Button, Popconfirm } from 'antd';
import React, { useState } from 'react';
import { IoArrowBack } from 'react-icons/io5';
import { styled } from 'styled-components';

interface TopHeaderProps {
	onGoBack: () => void;
	hasUnsaveChanges: boolean;
	title: string;
	canSave: boolean;
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
const TopHeader: React.FC<TopHeaderProps> = ({
	onGoBack,
	hasUnsaveChanges,
	iconUrl,
	title,
	canSave,
	onSave
}) => {
	const [isUnsavedMsgPopupOpen, setUnsavedMsgPopupOpen] = useState(false);
	return (
		<Container>
			<Popconfirm
				title="Are you sure?"
				description="You have unsaved changes!"
				okText="Yes"
				cancelText="No"
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

			<div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', gap: '6px' }}>
				{iconUrl && (
					<img style={{ width: '28px', height: '28px', borderRadius: '6px' }} src={iconUrl}></img>
				)}
				{title}
			</div>
			<Button disabled={!canSave} type="primary" onClick={() => onSave()}>
				Save
			</Button>
		</Container>
	);
};

export default TopHeader;
