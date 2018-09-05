import org.junit.Test;

public class test {
    @Test
    public void test() {
        String str = "abcd12345ed123ss123456789";

        char[] c = str.toCharArray();

        for (char cc : c) {
            System.out.println(Character.isDigit(cc));
        }

    }
}
