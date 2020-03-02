namespace Exir.Remark
{
    public static class RemarkBooleanState
    {
        public static string Build(
              bool? value
            , string tooltipTrue = "تایید شده"
            , string tooltipFalse = "رد شده"
            , string tooltipNull = "بررسی نشده"
        )
        {
            return $@"<span
                class='fa fa-circle text-{(value.HasValue ? ((bool) value ? "success" : "danger") : "black")}'
                data-toggle='tooltip'
                data-placement='top'
                title=''
                data-original-title='{(value.HasValue ? ((bool) value ? tooltipTrue : tooltipFalse) : tooltipNull)}'></span>";
        }
    }
}
