import { Collapse, CollapseProps, Popconfirm, Space, Tooltip } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import { ExecutorInput } from 'api/actionExecutorApi';
import IconButton from 'components/UI/IconButton/IconButton';
import { useCallback, useState } from 'react';
import { AiOutlineExclamationCircle, AiOutlinePlus } from 'react-icons/ai';
import { IoMdTrash } from 'react-icons/io';
import { IoDuplicate } from 'react-icons/io5';
import styled, { CSSProperties, useTheme } from 'styled-components';
import { useCounter } from 'usehooks-ts';
import { MASK_ENCRYPTED, createNewInput, duplicateInput } from 'utils/executorUtils';
import InputEditor from './InputEditor/InputEditor';

export interface InputsEditorProps {
	inputs: ExecutorInput[];
	inputSchema: FieldInfo[];
	onChangeInputs: (inputs: ExecutorInput[]) => void;
	style?: CSSProperties;
}
const collapseTitleClassname = 'CollapseTitleClass';
const CollapseWrapper = styled.div`
	& .${collapseTitleClassname} {
		align-items: center !important;
	}
`;
const InputsEditor = ({ inputs, inputSchema, onChangeInputs, style }: InputsEditorProps) => {
	const { count: nextInputId, increment: incNextInputId } = useCounter(0);
	const [inputsIds, setInputIds] = useState(() => inputs.map((_) => Math.random() * 1000000));
	console.log(inputs);
	const onDeleteInput = useCallback(
		(idx: number) => {
			const id = inputsIds[idx];
			setInputIds(inputsIds.filter((a) => a !== id));
			onChangeInputs(inputs.filter((_, idxx) => idxx !== idx));
		},
		[inputs, inputsIds, onChangeInputs] /// in order to useCallback had any sense, we should remove inputs from dependencies
	);
	const onAddInput = () => {
		setInputIds([...inputsIds, nextInputId]);
		onChangeInputs([...inputs, createNewInput(inputSchema)]);
		incNextInputId();
	};
	const onDuplicateInput = useCallback(
		(idx: number) => {
			const inputCopy = duplicateInput(inputs[idx], inputSchema);

			const newId = nextInputId;
			incNextInputId();

			const newIds = [...inputsIds];
			newIds.splice(idx + 1, 0, newId);

			const newInputs = [...inputs];
			newInputs.splice(idx + 1, 0, inputCopy);

			setInputIds(newIds);
			onChangeInputs(newInputs);
		},
		[incNextInputId, inputSchema, inputs, inputsIds, nextInputId, onChangeInputs]
	);
	const isInputValid = (idx: number) => {
		const input = inputs[idx];
		if (!input) return false;
		return Object.values(input).every((field) => field?.isValid);
	};
	const theme = useTheme();
	const items: CollapseProps['items'] =
		// useMemo(() =>
		inputs.map((input, idx) => ({
			key: inputsIds[idx],
			label: `Input ${idx + 1} (${Object.entries(input)
				.map(
					([name, val]) =>
						`${name}: ${
							inputSchema.some((a) => a.name === name && a.type === 'Secret')
								? MASK_ENCRYPTED
								: val?.value
						}`
				)
				.join(', ')})`,
			headerClass: collapseTitleClassname,
			style: {},
			extra: (
				<Space>
					<IconButton
						style={{ fontSize: '16px', padding: '10px', color: theme.infoColor }}
						type="text"
						onClick={(e) => onDuplicateInput(idx)}
					>
						{/* <IoMdTrash /> */}
						<IoDuplicate />
						Duplicate
					</IconButton>
					<Popconfirm
						title="Delete input"
						description="Are you sure to delete this input?"
						onConfirm={() => onDeleteInput(idx)}
						okText="Yes"
						cancelText="No"
						onPopupClick={(e) => e.stopPropagation()}
					>
						<IconButton
							style={{ fontSize: '16px', padding: '10px' }}
							danger
							type="text"
							onClick={(e) => e.stopPropagation()}
						>
							<IoMdTrash />
							Delete
						</IconButton>
					</Popconfirm>
					{!isInputValid(idx) && (
						<div
							style={{
								color: theme.warningColor,
								fontSize: '20px',
								display: 'flex',
								alignItems: 'center'
							}}
						>
							<Tooltip title="Input is not correct" color="volcano">
								<AiOutlineExclamationCircle />
							</Tooltip>
						</div>
					)}
				</Space>
			),
			children: (
				<InputEditor
					key={inputsIds[idx]}
					input={input}
					inputSchema={inputSchema}
					onChangeInput={(input) => {
						const newInputs = [
							...inputs.slice(0, idx),
							input,
							...inputs.slice(idx + 1, inputs.length)
						];
						onChangeInputs(newInputs);
						return newInputs;
					}}
				/>
			)
		})) as CollapseProps['items'];
	// 	[
	// 		inputSchema,
	// 		inputs,
	// 		inputsIds,
	// 		onChangeInputs,
	// 		onDeleteInput,
	// 		onDuplicateInput,
	// 		theme.infoColor
	// 	]
	// );

	return (
		<div style={{ ...style }}>
			<div
				style={{
					fontSize: '24px',
					fontWeight: 400,
					display: 'flex',
					flexDirection: 'row',
					gap: '5px',
					marginBottom: '12px'
				}}
			>
				Inputs
				<IconButton type="text" shape="circle" onClick={onAddInput} style={{ fontSize: '20px' }}>
					<AiOutlinePlus />
				</IconButton>
			</div>
			<CollapseWrapper>
				<Collapse bordered={true} accordion items={items} />
			</CollapseWrapper>
		</div>
	);
};

export default InputsEditor;
