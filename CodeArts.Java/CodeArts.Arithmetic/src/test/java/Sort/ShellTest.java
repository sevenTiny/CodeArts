package Sort;

import Common.CommonData;
import org.junit.Test;

public class ShellTest {
    @Test
    public void test() {
        Shell.Sort(CommonData.OUT_OF_ORDER_ARRAY);

        for (int a : CommonData.OUT_OF_ORDER_ARRAY) {
            System.out.println(a);
        }
    }
}
