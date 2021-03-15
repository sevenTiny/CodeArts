package Sort;

import Common.CommonData;
import org.junit.Test;

public class QuickTest {
    @Test
    public void test() {
        Quick.Sort(CommonData.OUT_OF_ORDER_ARRAY);

        for (int a : CommonData.OUT_OF_ORDER_ARRAY) {
            System.out.println(a);
        }
    }
}
