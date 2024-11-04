export const CheckIcon = ({height, width}: { height: number, width: number }) => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width={width} height={height} viewBox="0 0 24 24" fill="none">
      <circle opacity="0.5" cx="12" cy="12" r="10" stroke="#1C274C" strokeWidth="1.5"/>
      <path d="M8.5 12.5L10.5 14.5L15.5 9.5" stroke="#1C274C" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"/>
    </svg>);
};
